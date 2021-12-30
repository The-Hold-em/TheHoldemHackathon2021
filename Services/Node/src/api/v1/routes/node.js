const express = require("express");
const router = express.Router();

const NodeController = require("../controllers/node");

router.post("/receiveVoteList", NodeController.receive_vote_list);
router.post("/receiveBlock", NodeController.receive_block);
router.get("/getBlockchain", NodeController.get_block_chain);
router.get("/getLastBlock", NodeController.get_last_block);
router.get("/getGenesisBlock", NodeController.get_genesis_block);
router.get("/getBlock/:id", NodeController.get_block);
router.post("/voteResults", NodeController.vote_results);

module.exports = router;
