
import { run } from './grpc-storage-service';
import { SERVER_ADDRESS } from "./grpc-storage-service";

run().then(r => console.log(`Started Server at ${SERVER_ADDRESS}`))



