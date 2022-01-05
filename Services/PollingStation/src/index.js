const http = require("http");
const app = require("./api/v1/app");

const Database = require("./api/v2/services/mongodb");

Database.connect();
//Database.close();
const port = process.env.PORT || 3001;

const server = http.createServer(app);

server.listen(port);
