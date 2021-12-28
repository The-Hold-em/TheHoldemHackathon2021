require("dotenv").config();
const express = require("express");
const app = express();
const morgan = require("morgan");
const bodyParser = require("body-parser");

const NodeRoutes = require("./api/v1/routes/node");

const Cors = require("./api/v1/middlewares/cors");
const InternalServerError = require("./api/v1/middlewares/500");
const NotFound = require("./api/v1/middlewares/404");

app.use(morgan("dev"));
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(Cors.cors);
// Routes which should handle requests
//---
app.use("/node", NodeRoutes);

const { Blockchain } = require("../src/api/v1/models/blockchain");

let testElection = new Blockchain();
testElection.addVoteList({
  pollingStationPublicKey:
    "046423d73500c09b602c299eb0586a2a6581fa4151211388d32077e3dd6420b43c55a205a5b23126fd39ddbed48991c2dee975358d79bbfbf75a5374dad41f217f",
  signature:
    "304502200a57d487028a7c92221740b10b1aa5c97298df565c0e4511b7d7b8fb77529aed022100f8e79de2081bb5eec4a012790ec02838058bc1e58a7725caa7b188f512782291",
  voteList: [
    {
      publicKey:
        "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
      candinateId: "1.Aday",
      signature:
        "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409",
    },
    {
      publicKey:
        "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
      candinateId: "1.Aday",
      signature:
        "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409",
    },
    {
      publicKey:
        "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
      candinateId: "1.Aday",
      signature:
        "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409",
    },
  ],
});
testElection.mineVoteList();

console.log(testElection.isChainVaild());

app.use(InternalServerError.internal_server_err);
app.use(NotFound.not_found);

module.exports = app;
