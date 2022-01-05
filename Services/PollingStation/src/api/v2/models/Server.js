const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const serverSchema = new Schema({
  serverName: {
    type: String,
    required: false,
    maxlength: [130, "Server name max length's is 130 characters"],
  },
  serverStatus: {
    type: Boolean,
    required: true,
    default: false,
  },
  serverStartTime: {
    type: Date,
  },
  serverEndTime: {
    type: Date,
  },
});

module.exports = {
  Server: mongoose.model("Server", serverSchema),
};
