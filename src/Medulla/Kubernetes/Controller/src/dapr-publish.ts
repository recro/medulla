import { DaprClient, CommunicationProtocolEnum } from 'dapr-client';

/**
 * Publish will send a publish event to the DAPR instance.
 * @param pubsubName the name of the dapr pubsub
 * @param topicName the name of the dapr topic
 * @param dataMessage the data to send
 */
const publish = async (pubsubName :string, topicName :string, dataMessage :any) => {
    const client = new DaprClient(process.env.DAPR_HOST || "127.0.0.1", process.env.DAPR_HTTP_PORT || "3500", CommunicationProtocolEnum.HTTP);
    console.log("Data Message:" + dataMessage)
    await client.pubsub.publish(pubsubName, topicName, dataMessage);
}

export default publish
