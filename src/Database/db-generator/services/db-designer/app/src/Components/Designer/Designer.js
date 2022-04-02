import * as React from 'react';
import Box from '@mui/material/Box';
import logo from "../../logo.svg";
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { v4 as uuidv4 } from 'uuid';
import axios from 'axios';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';

const getErd = async (state) => {

    const result = await axios.post("http://localhost:3001/generate-erd", {
        db: [{ models: state.models }]
    })

    console.log({ result: result.data })

    return result.data

}

const generateForm = (state, pageObject) => {

    let models = []

    for (let i = 0; i < state.models.length; i++) {

        let columns = []
        for (let columnI = 0; columnI < state.models[i].columns.length; columnI++) {
            columns.push(
                (
                    <div key={state.models[i].columns[columnI].key}>
                        <TextField onChange={async (e) => {
                            pageObject.state.models[i].columns[columnI].field = e.target.value
                            pageObject.setState({
                                models: pageObject.state.models
                            })
                        }} id="outlined-basic" label="column Name" variant="standard" />
                        <TextField onChange={async (e) => {
                            pageObject.state.models[i].columns[columnI].type = e.target.value
                            pageObject.setState({
                                models: pageObject.state.models
                            })
                        }} id="outlined-basic" label="column Type" variant="standard" />
                    </div>
                )
            )
        }


        models.push(
            (

                <Card style={{ marginTop: "20px"}} sx={{ minWidth: 275, backgroundColor: 'lightgray', paddingTop: '10px', paddingBottom: '10px' }}>
                    <CardContent style={{padding: 0}}>
                        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                            Model
                        </Typography>
                        <div key={state.models[i].key}>
                            <div>
                                <TextField value={pageObject.state.models[i].name} onChange={(e) => {
                                    pageObject.state.models[i].name = e.target.value
                                    pageObject.setState({
                                        models: pageObject.state.models
                                    })
                                }} id="outlined-basic" label="Model Name" variant="outlined" />
                                { columns }
                                <Button onClick={async () => {
                                    let columns = pageObject.state.models[i].columns.push({ key: uuidv4(), field: "test", type: "string" })
                                    console.log(columns)
                                    pageObject.setState({
                                        models: pageObject.state.models
                                    })
                                }} variant="text">Add Column</Button>
                            </div>

                            <Button onClick={async () => {
                                let columns = pageObject.state.models.push({ key: uuidv4(), name: "test", columns: [] })
                                console.log(columns)
                                pageObject.setState({
                                    models: pageObject.state.models
                                })
                            }} variant="outlined">Add Model</Button>
                        </div>
                    </CardContent>
                </Card>
            )
        )

    }

    return (models)
}

export default class Designer extends React.Component {
    constructor() {
        super()
        this.state = {
            svg: '',
            models: [
                {
                    key: 'test',
                    name: "test",
                    columns: [

                    ]
            },

            ]
        }
    }

    render() {
        return (
            <div style={{ display: "flex" }}>
                <img style={{ width: "20vw"}} src={logo} className="App-logo" alt="logo" />
                <Box
                    sx={{
                        width: "60vw",
                        height: "100vh",
                        backgroundColor: 'white',
                    }}
                >
                    {/*<iframe style={{width: "100vw", height: "100vh"}} src={"http://localhost:3001/generate-erd"} ></iframe>*/}
                    <div dangerouslySetInnerHTML={ { __html: this.state.svg }}></div>
                </Box>
                <Box
                    sx={{
                        width: "20vw",
                        height: "100vh",
                        backgroundColor: 'white',
                        paddingTop: "1em",
                        overflow: "scroll",
                        '&:hover': {
                            backgroundColor: 'white',
                            opacity: [1.0, 1.0, 1.0],
                        },
                    }}
                >
                    <Button onClick={async () => {
                        let svg = await getErd(this.state)
                        this.setState({
                            svg: svg
                        })
                    }} variant="outlined">Sync</Button>
                    <div style={{height: "100px"}}></div>


                    { generateForm(this.state, this) }
                </Box>
            </div>
            )
    }





}