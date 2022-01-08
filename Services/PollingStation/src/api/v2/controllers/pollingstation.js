const { Server } = require("../models/Server");
const { ElectionTimer } = require("../services/timer");
const { State } = require("../helpers/state");

exports.start_election = (req, res, next) => {
  const pollingStationID = req.body.pollingStationID;
  const period = req.body.period;
  const state = true;

  const filter = {
    _id: pollingStationID,
  };
  const update = {
    serverStatus: state,
    serverStartTime: new Date()
      .toISOString()
      .replace(/T/, " ") // replace T with a space
      .replace(/\..+/, ""), // delete the dot and everything after
  };

  Server.findByIdAndUpdate(filter, update, { new: true });
  State.electionTimer = new ElectionTimer(state, period);
  State.electionTimer.startElection();
  return res.status(200).json({
    Message: "Polling station started",
  });
};

exports.stop_election = (req, res, next) => {
  const pollingStationID = req.body.pollingStationID;
  const state = false;

  const filter = {
    _id: pollingStationID,
  };
  const update = {
    serverStatus: state,
    serverEndTime: new Date()
      .toISOString()
      .replace(/T/, " ") // replace T with a space
      .replace(/\..+/, ""), // delete the dot and everything after
  };
  Server.findByIdAndUpdate(filter, update, { new: true });
  State.electionTimer.timer.clearInterval();
  return res.status(200).json({
    Message: "Polling station stoped",
  });
};

exports.get_server_status = (req, res, next) => {
  const pollingStationID = req.body.pollingStationID;
  const filter = {
    _id: pollingStationID,
  };
  Server.find(filter)
    .then((server) => {
      res.status(200).json({
        Status: server.serverStatus,
      });
    })
    .catch((err) => {
      res.status(404).json({
        Message: err,
      });
    });
};
