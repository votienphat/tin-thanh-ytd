'use strict';


var mongoose = require('mongoose'),
    CodeDocs = mongoose.model('CodeDocs'),
    utility = require('../../custom_modules');

exports.Search = function(req, res) {
    res.json({"a":req.params.keyword, "b": 9999999999});
};

// Lấy danh sách gợi ý
exports.Suggest = function(req, res) {
    var keyword = req.params.keyword;

    utility.api.AllowCrossDomain(res, 'http://localhost:9000');

    if (!utility.main.IsNullOrUndefined(keyword, 2)){
        var result = [];

        // Push tạm dữ liệu
        result.push({title: 'Giới thiệu NodeJS', id: 1});
        result.push({title: 'MongoDB là gì?', id: 2});
        result.push({title: 'Tại sao phải sử dụng RESTful?', id: 3});
        result.push({title: 'Tìm kiếm từ khóa ', keyword: keyword});

        res.json(result);
    }
    else{
        res.json({result: 'Lỗi keyword'});
    }
};
