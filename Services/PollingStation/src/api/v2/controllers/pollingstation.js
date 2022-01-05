const { Vote, CalculateHash, IsVaild } = require("../models/Vote");
const { Server } = require("../models/Server");
const { ElectionTimer } = require("../services/timer");

exports.recevie_vote = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;

  if (IsVaild()) {
    Vote.create({
      votePublicKey: publicKey,
      candinateId: candinateId,
      voteSignature: signature,
      voteHash: CalculateHash(publicKey + candinateId + signature),
    });
    return res.status(200).json({
      Message: "This vote validation is success",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};

exports.start_polling_station = (req, res, next) => {
  const pollingStationID = req.body.pollingStationID;
  const period = req.body.period;
  const state = true;

  const filter = {
    _id = pollingStationID
  }
  const update = {
    serverStatus:state,
    serverStartTime:new Date().toISOString().
    replace(/T/, ' ').      // replace T with a space
    replace(/\..+/, '')     // delete the dot and everything after
  }
  
  Server.findByIdAndUpdate(filter,update,{new:true});
  return res.status(200).json({
    Message: "Polling station started",
  });

  if (state === 1) {
    State.electionTimer = new ElectionTimer(state, period);
    State.electionTimer.startElection();
    return res.status(200).json({
      Message: "election started",
    });
  } else if (state === 0) {
    State.serverStatus = false;
    State.electionTimer.timer.clearInterval();
    return res.status(200).json({
      Message: "Election stopped",
    });
  }
};

exports.stop_polling_station = (req, res, next) => {
  const pollingStationID = req.body.pollingStationID;
  const period = req.body.period;
  const state = false;
  
  const filter = {
    _id = pollingStationID
  }
  const update = {
    serverStatus:state,
    serverEndTime:new Date().toISOString().
    replace(/T/, ' ').      // replace T with a space
    replace(/\..+/, '')     // delete the dot and everything after
  }
  
  Server.findByIdAndUpdate(filter,update,{new:true});
  return res.status(200).json({
    Message: "Polling station started",
  });
};

exports.get_server_status = (req, res, next) => {
  return res.status(200).json({
    Status: State.serverStatus,
  });
};
