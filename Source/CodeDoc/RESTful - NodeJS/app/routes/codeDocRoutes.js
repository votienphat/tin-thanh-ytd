'use strict';
module.exports = function(app) {
    var codeDoc = require('../controllers/codeDocController');

    app.route('/code/:keyword')
        .get(codeDoc.Search);
};