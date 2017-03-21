'use strict';
var mongoose = require('mongoose'),
    mongoosastic = require("mongoosastic");
var Schema = mongoose.Schema;


var codeDocSchema = new Schema({
    name: {
        type: String,
        Required: 'Kindly enter the name of the task'
    },
    Created_date: {
        type: Date,
        default: Date.now
    },
    status: {
        type: [{
            type: String,
            enum: ['pending', 'ongoing', 'completed']
        }],
        default: ['pending']
    }
});

codeDocSchema.plugin(mongoosastic,{
    host:"localhost",
    port: 9200,
    protocol: "http",
    auth: "username:password"
//  ,curlDebug: true
});

module.exports = mongoose.model('CodeDocs', codeDocSchema);