const sendVoteList = require("../services/pollingstation");
module.exports.send_vote_list = (res, req, next) => {
  sendVoteList.SendVoteList;
  console.log("a");
};
