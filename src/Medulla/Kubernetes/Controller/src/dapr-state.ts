import axios from "axios";

/**
 * setState will save any object in value to a key stored in DAPR redis.
 * @param key the key to save state
 * @param value the value to save state
 */
const setState = async (key :string, value :any) => {
    await axios.post(`http://${process.env.DAPR_HOST}:${process.env.DAPR_HTTP_PORT}/v1.0/state/statestore`, [{ "key": key, "value": value}]);
};

/**
 * getState will get any object in value from a key stored in DAPR redis.
 * @param key the key to get state
 */
const getState = async (key :string) => {
    const response = await axios.get(`http://localhost:3500/v1.0/state/statestore/${key}`)
    return response.data;
}

export {
    getState,
    setState
}
