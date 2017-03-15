'use strict';


var mongoose = require('mongoose'),
    CodeDocs = mongoose.model('CodeDocs');

exports.Search = function(req, res) {
    res.json({"a":req.params.keyword, "b": 9999999999});
};
