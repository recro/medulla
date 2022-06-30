﻿using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using DatabaseDesigner.Core.Models;
using DatabaseDesigner.Wasm.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DatabaseDesigner.Wasm.Components.Database;
using DatabaseDesigner.Wasm.Components.Database.TableNode;
using DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode;
using DatabaseDesigner.Wasm.Components.RegisteredComponents.Footer;
using DatabaseDesigner.Wasm.Components.RegisteredComponents.Header;

namespace DatabaseDesigner.Wasm.Pages
{
    public partial class Index : IDisposable
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public Diagram Diagram { get; } = new Diagram(new DiagramOptions
        {
            GridSize = 80,
            AllowMultiSelection = false,
            Links = new DiagramLinkOptions
            {
                Factory = (diagram, sourcePort) =>
                {
                    return new LinkModel(sourcePort, null)
                    {
                        Router = Routers.Orthogonal,
                        PathGenerator = PathGenerators.Straight,
                    };
                }
            }
        });

        public void Dispose()
        {
            Diagram.Links.Added -= OnLinkAdded;
            Diagram.Links.Removed -= Diagram_LinkRemoved;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Diagram.RegisterModelComponent<Table, TableNode>();
            Diagram.RegisterModelComponent<Database, DatabaseNode>();
            Diagram.RegisterModelComponent<DatabaseDesigner.Core.Models.TableColumn, TableColumnNode>();
            Diagram.RegisterModelComponent<DatabaseDesigner.Core.Models.RegisteredComponents.Footer, Footer>();
            Diagram.RegisterModelComponent<DatabaseDesigner.Core.Models.RegisteredComponents.Header, Header>();
            Diagram.RegisterModelComponent<DatabaseDesigner.Core.Models.Cron, DatabaseDesigner.Wasm.Components.Cron.Cron>();
            Diagram.RegisterModelComponent<DatabaseDesigner.Core.Models.RegisteredComponents.PageWrapper, DatabaseDesigner.Wasm.Components.RegisteredComponents.PageWrapper.PageWrapper>();
            Diagram.RegisterModelComponent<DatabaseDesigner.Core.Models.RegisteredComponents.DataTable, DatabaseDesigner.Wasm.Components.DataTable.DataTable>();
            // Diagram.Nodes.Add(new Table());
            // Diagram.Nodes.Add(new Table());
            Diagram.Nodes.Add(new Database());
            // Diagram.Nodes.Add(new DatabaseDesigner.Core.Models.RegisteredComponents.Footer());
            // Diagram.Nodes.Add(new Database());
            // Diagram.Nodes.Add(new DatabaseDesigner.Core.Models.TableColumn());

            // Console.WriteLine(Diagram.Nodes[0].Position.Add(100, 500));

            Diagram.Links.Added += OnLinkAdded;
            Diagram.Links.Removed += Diagram_LinkRemoved;
        }

        public void AddDatabase()
        {
            Diagram.Nodes.Add(new Database());
        }

        private void OnLinkAdded(BaseLinkModel link)
        {
            link.TargetPortChanged += OnLinkTargetPortChanged;
        }

        private void OnLinkTargetPortChanged(BaseLinkModel link, PortModel oldPort, PortModel newPort)
        {
            link.Labels.Add(new LinkLabelModel(link, "1..*", -40, new Point(0, -30)));
            link.Refresh();

            ((newPort ?? oldPort) as ColumnPort).Column.Refresh();
        }

        private void Diagram_LinkRemoved(BaseLinkModel link)
        {
            link.TargetPortChanged -= OnLinkTargetPortChanged;

            if (!link.IsAttached)
                return;

            var sourceCol = (link.SourcePort as ColumnPort).Column;
            var targetCol = (link.TargetPort as ColumnPort).Column;
            (sourceCol.Primary ? targetCol : sourceCol).Refresh();
        }

        private void NewTable()
        {
            Diagram.Nodes.Add(new Table());
        }

        private async Task ShowJson()
        {
            Console.WriteLine("Test");
            var json = JsonConvert.SerializeObject(new
            {
                Nodes = Diagram.Nodes.Cast<object>(),
                Links = Diagram.Links.Cast<object>()
            }, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            await JSRuntime.InvokeVoidAsync("console.log", json);
        }

        private void Debug()
        {
            Console.WriteLine(Diagram.Container);
            foreach (var port in Diagram.Nodes.ToList()[0].Ports)
                Console.WriteLine(port.Position);
        }
    }
}