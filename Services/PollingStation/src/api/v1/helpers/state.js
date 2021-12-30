const { ElectionTimer } = require("../services/timer");
exports.votes = [];
exports.voteListHash = [];
exports.electionTimer = new ElectionTimer();
exports.serverStatus = false;
