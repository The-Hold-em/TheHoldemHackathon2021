const mongoose = require("mongoose");
const Vote = require("../models/mongoose/voteSchema");

class Database {
  connection = mongoose.connection;

  constructor() {
    try {
      this.connection
        .on("open", console.info.bind(console, "Database connection: open"))
        .on("close", console.info.bind(console, "Database connection: close"))
        .on(
          "disconnected",
          console.info.bind(console, "Database connection: disconnecting")
        )
        .on(
          "disconnected",
          console.info.bind(console, "Database connection: disconnected")
        )
        .on(
          "reconnected",
          console.info.bind(console, "Database connection: reconnected")
        )
        .on(
          "fullsetup",
          console.info.bind(console, "Database connection: fullsetup")
        )
        .on("all", console.info.bind(console, "Database connection: all"))
        .on("error", console.error.bind(console, "MongoDB connection: error:"));
    } catch (error) {
      console.error(`Mongodb connection error: ${error}`);
    }
  }
  /** Connect local mongodb  */
  async connect() {
    var dbname = process.env.MONGODB_DB;
    try {
      await mongoose.connect(
        process.env.MONGODB_LOCAL_CONNECTION + `/${dbname}`,
        {
          useNewUrlParser: true,
          useUnifiedTopology: true,
        }
      );
    } catch (error) {
      console.error(`Mongodb connect error: ${error}`);
    }
  }
  async close() {
    try {
      await this.connection.close();
    } catch (error) {
      console.error(`Mongodb close error: ${error}`);
    }
  }
}

module.exports = new Database();
