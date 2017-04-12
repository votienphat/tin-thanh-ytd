exports.AllowCrossDomain = function(res, domains){
    res.header("Access-Control-Allow-Origin", Array.isArray(domains) ? domains.join() : domains);
    res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
    res.header("Access-Control-Allow-Headers", "X-Requested-With,     Content-Type");
};
