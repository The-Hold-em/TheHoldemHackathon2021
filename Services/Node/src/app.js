require("dotenv").config();
const express = require("express");
const app = express();
const morgan = require("morgan");
const bodyParser = require("body-parser");

const NodeRoutes = require("./api/v1/routes/node");

const Cors = require("./api/v1/middlewares/cors");
const InternalServerError = require("./api/v1/middlewares/500");
const NotFound = require("./api/v1/middlewares/404");

app.use(morgan("dev"));
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(Cors.cors);
// Routes which should handle requests
//---
app.use("/node", NodeRoutes);

app.use(InternalServerError.internal_server_err);
app.use(NotFound.not_found);

module.exports = app;
