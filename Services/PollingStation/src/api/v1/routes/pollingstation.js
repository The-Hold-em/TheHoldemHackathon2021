const express = require("express");
const router = express.Router();

const PollingStationController = require("../controllers/pollingstation");

router.post("/recevieVote", PollingStationController.recevie_vote);

module.exports = router;
