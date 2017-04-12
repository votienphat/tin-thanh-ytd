exports.String2Bytes = function(str,offset,msg){
    for (var j = 0; j < str.length; j++) {
        if(offset>=msg.byteLength)break;
        var charCode = str.charCodeAt(j);
        msg.setUint16(offset, charCode, __endian);
        offset += 2;
    }
    if(offset<msg.byteLength){//break string
        msg.setUint16(offset, 0, __endian);
        offset += 2;
    }
    return offset;
};

exports.Bytes2String = function(offset,msg){
    var str = '';
    for(;offset<msg.byteLength;i++){
        var charCode = msg.getUint16(offset,__endian);
        if(charCode==0)break;
        str +=String.fromCharCode(charCode);
    }

    return {offset:offset,str:str};
};

exports.PrepareData = function(DataLength) {
    return new DataView(new ArrayBuffer(DataLength))
};

exports.String2Buffer = function(str,endcoding){
    var bufHeadStr = new Buffer(2);
    bufHeadStr.writeInt16LE(str.length,0);
    var bufStr = new Buffer(str,endcoding);
    return Buffer.concat([bufHeadStr,bufStr]);
};

exports.AddZero = function (num) {
    return ('0' + num).slice(-2);
}

/**
 * Kiểm tra đối tượng có null hay không
 * @param obj
 * @returns {boolean}
 * @constructor
 */
exports.IsNullOrUndefined = function (obj) {
    return obj == null || typeof obj == 'undefined';
};

/**
 * Kiểm tra đối tượng chuỗi có null hay không và độ dai
 * @param obj
 * @returns {boolean}
 * @constructor
 */

/**
 * Kiểm tra đối tượng có null hay không và độ dài có lớn hơn độ dài quy định không
 * @param obj
 * @returns {boolean}
 * @constructor
 */
exports.IsNullOrUndefined = function (obj, length) {
    length = length || 0;
    var result = obj == null || typeof obj == 'undefined';

    if (!result){
        return obj.length < length;
    }

    return result;
};

/**
 * Lấy giá trị mặc định của kiểu số
 * @param obj
 * @returns {int}
 * @constructor
 */
exports.NumberGetDefaultNullOrUndefined = function (obj) {
    var isNull = (obj == null || typeof obj == 'undefined');
    if (isNull)
        return 0;
    return obj;
};

/**
 * Lấy giá trị mặc định của kiểu số
 * @param obj
 * @returns {string}
 * @constructor
 */
exports.StringGetDefaultNullOrUndefined = function (obj) {
    var isNull = (obj == null || typeof obj == 'undefined');
    if (isNull)
        return '';
    return obj;
};

/**
 * Format số thành chuỗi có dấu phân cách
 * @param str
 * @returns {string}
 * @constructor
 */
exports.FormatCoin = function (str) {

    var seperate = '.';

    if (typeof str === "number")
        str = str.toString();

    var strResult = "";
    var count = -1;
    var stringLength = str.length;

    for (var i = 0; i < stringLength; i++) {
        count++;
        if (count == 3) {
            count = 0;
            if (parseInt(str.charAt(stringLength - (i + 1)), 10).toString() != "NaN" && str.charAt(stringLength - (i + 1)) != "-") {
                strResult += seperate + str.charAt(stringLength - (i + 1));
            }
            else {
                strResult += str.charAt(stringLength - (i + 1));
            }
        }
        else {
            strResult += str.charAt(stringLength - (i + 1));
        }
    }

    var s1 = "";
    var strResultLength = strResult.length;

    for (var j = 0; j < strResultLength; j++) {
        s1 += strResult.charAt(strResultLength - (j + 1));
    }

    return s1;
};

/**
 * Lấy thời gian local
 * @param str
 * @returns {string}
 * @constructor
 */
exports.toLocalTime = function(time) {
    var d = new Date();
    if (!this.IsNullOrUndefined(time))
        d = new Date(time);
    var offset = (new Date().getTimezoneOffset() / 60) * -1;
    var n = new Date(d.getTime() + (offset*60*60*1000));
    return n;
};