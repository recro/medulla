
import axios from "axios";


const setState = async (key :string, value :any) => {
    await axios.post(`http://${process.env.DAPR_HOST}:${process.env.DAPR_HTTP_PORT}/v1.0/state/statestore`, [{ "key": key, "value": value}]);
};

const getState = async (key :string) => {
    const response = await axios.get(`http://localhost:3500/v1.0/state/statestore/${key}`)
    return response.data;
}



export {
    getState,
    setState
}