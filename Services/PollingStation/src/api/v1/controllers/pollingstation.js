const SHA256 = require("crypto-js/sha256.js");
var State = require("../helpers/state");
const { Vote } = require("../models/vote");
const { ElectionTimer } = require("../services/timer");

exports.recevie_vote = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;
  var vote = new Vote(publicKey, candinateId, signature);
  if (vote.isValid()) {
    State.votes.push({
      publicKey: req.body.publicKey,
      candinateId: req.body.candinateId,
      signature: req.body.signature,
    });
    State.voteListHash.push(
      SHA256(publicKey + candinateId + signature).toString()
    );
    return res.status(200).json({
      Message: "This vote is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};

exports.start_polling_station = (req, res, next) => {
  const state = req.body.state;
  const period = req.body.period;

  if (state === 1) {
    State.electionTimer = new ElectionTimer(state, period);
    State.electionTimer.startElection();
    return res.status(200).json({
      Message: "election started",
    });
  } else if (state === 0) {
    State.electionTimer.timer.clearInterval();
    return res.status(200).json({
      Message: "Election stopped",
    });
  }
};
