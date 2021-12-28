const State = require("../../helpers/state");

exports.sendVoteListToNode = (req, res, next) => {
  console.log("==============================");
  console.log("Request Body: " + JSON.stringify(req.body, undefined, 2));
  console.log("\n");
  console.log(State.votes);
  console.log("==============================\n");
  State.votes = [];
  State.voteListHash = [];
  return req.bod;
};
