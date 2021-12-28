const SHA256 = require("crypto-js/sha256.js");
const ValidateSignature = require("../helpers/validatesignature");

exports.receive_vote_list = (req, res, next) => {
  const pollingStationPublicKey = req.body.pollingStationPublicKey;
  const signature = req.body.signature;
  const voteList = req.body.voteList;
  //console.log(JSON.stringify(req.body, undefined, 2));
  if (
    ValidateSignature.validate_signature(
      pollingStationPublicKey,
      signature,
      voteList
    )
  ) {

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
