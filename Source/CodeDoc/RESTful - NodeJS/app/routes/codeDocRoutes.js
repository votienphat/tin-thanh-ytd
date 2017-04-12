'use strict';
module.exports = function(app) {
    var codeDoc = require('../controllers/codeDocController');

    app.route('/suggest/:keyword')
        .get(codeDoc.Suggest);
};