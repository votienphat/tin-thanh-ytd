var express = require('express'),
    app = express(),
    port = process.env.PORT || 3000,
    mongoose = require('mongoose'),
    bodyParser = require('body-parser'),
    codeDoc = require('./app/models/codeDocModel');

mongoose.Promise = global.Promise;
mongoose.connect('mongodb://127.0.0.1/CodeDocs');


app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

var routes = require('./app/routes/codeDocRoutes');
routes(app);


app.listen(port);

console.log('todo list RESTful API server started on: ' + port);