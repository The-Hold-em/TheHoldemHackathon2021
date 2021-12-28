const express = require("express");
const router = express.Router();

const PollingStationController = require("../controllers/pollingstation");
const PollingStationControllerTest = require("../controllers/tests/pollingstation");

router.post("/recevieVote", PollingStationController.recevie_vote);

router.post(
  "/sendVoteListToNode",
  PollingStationControllerTest.sendVoteListToNode
);
module.exports = router;
