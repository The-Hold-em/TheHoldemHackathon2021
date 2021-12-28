const { VoteList } = require("../models/votelist");

exports.receive_vote_list = (req, res, next) => {
  const pollingStationPublicKey = req.body.pollingStationPublicKey;
  const signature = req.body.signature;
  const voteList = req.body.voteList;

  var voteListModel = new VoteList(
    pollingStationPublicKey,
    signature,
    voteList
  );

  if (voteListModel.isValid()) {
    return res.status(200).json({
      Message: "This vote list is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote list validation is failed",
    });
  }
};
exports.receive_block = (req, res, next) => {
  return res.status(200).json({
    Message: "receive_block succesfully worked",
  });
};

exports.get_block_chain = (req, res, next) => {
  return res.status(200).json({
    Message: "get_block_chain succesfully worked",
  });
};

exports.get_last_block = (req, res, next) => {
  return res.status(200).json({
    Message: "get_last_block succesfully worked",
  });
};

exports.get_genesis_block = (req, res, next) => {
  return res.status(200).json({
    Message: "get_genesis_block succesfully worked",
  });
};

exports.get_block = (req, res, next) => {
  return res.status(200).json({
    Message: "get_block succesfully worked",
  });
};

exports.vote_results = (req, res, next) => {
  return res.status(200).json({
    Message: "vote_results succesfully worked",
  });
};
