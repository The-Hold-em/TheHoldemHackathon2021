const express = require("express");
const router = express.Router();

const PollingStationController = require("../controllers/pollingstation");
const VoteController = require("../controllers/vote");
const PollingStationControllerTest = require("../controllers/tests/pollingstation");

router.post("/recevieVote", VoteController.recevie_vote);
//router.post("/startElection", PollingStationController.start_election);
//router.post("/stopElection", PollingStationController.stop_election);
router.get("/getServerStatus", PollingStationController.get_server_status);

router.post(
  "/sendVoteListToNode",
  PollingStationControllerTest.sendVoteListToNode
);
module.exports = router;
