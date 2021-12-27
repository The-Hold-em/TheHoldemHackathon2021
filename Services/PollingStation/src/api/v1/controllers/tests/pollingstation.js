const ValidateSignature = require("../../helpers/validatesignature");
var VoteList = require("../../helpers/state");

exports.recevie_vote = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;
  if (ValidateSignature.validate_signature(publicKey, candinateId, signature)) {
    VoteList.voteList.push("vote");
    return res.status(200).json({
      Message: "This vote is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};

exports.recevie_vote_test = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;
  if (ValidateSignature.validate_signature(publicKey, candinateId, signature)) {
    console.log(VoteList.voteList);
    return res.status(200).json({
      Message: "This vote is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};

exports.sendVoteListToNode = (req, res, next) => {
  console.log("==============================");
  console.log("Request Body: " + JSON.stringify(req.body, undefined, 2));
  console.log("\n");
  console.log("Vote List: " + VoteList.voteList);
  console.log("==============================\n");
  return req.bod;
};
