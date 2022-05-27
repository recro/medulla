using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;
using DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode;
using Blazor.Diagrams.Core;
using System;
using System.Collections.Generic;

namespace DatabaseDesigner.Wasm.Components.Database
{

    public class Type
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Column
    {
        
        public string Uuid { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }

        public bool IsEditing { get; set; } = false;

        public Column()
        {
            Guid myuuid = Guid.NewGuid();
            Uuid = myuuid.ToString();
        }

        public void Editing(bool isEditing)
        {
            IsEditing = isEditing;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        
        
    }
    
    
    
    
    
    
    
    
    public partial class DatabaseNode
    {
        [Parameter]
        public DatabaseDesigner.Core.Models.Database Node { get; set; }

        [Parameter]
        public bool InTray { get; set; } = false;

        [Parameter]
        public Diagram Diagram { get; set; }

        
        [Parameter]
        public Action<Diagram> AddToScene { get; set; }

        private Entity Entity { get; set; }

        public void QuickLoadingOnEntity() => Entity.QuickLoading();

        [Parameter]
        public RenderFragment Preview { get; set; }

        public List<Column> Columns { get; set; } = new();

        public bool EditTable { get; set; } = false;

        public string TableName { get; set; } = "TableName";


        public void AddColumn()
        {
            StopEditingAll(); 
            this.Columns.Add(new Column()
            {
                Name = "[New Column]",
                Type = new Type()
                {
                    Description = "String",
                    Name = "int"
                },
                IsEditing = true
            });
        }
        
        
        public void StopEditingAll()
        {
            foreach (var column in Columns)
            {
                column.IsEditing = false;
            }
        }

        public void DeleteColumn(Column column)
        {
            Columns.Remove(column);
        }









    }
}
