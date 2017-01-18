///创建人:张正泉
///创建日期:2015-08-03
///说明:
///各个模块公共类库和指令

//日期:20150817
//说明:公共模块中的静态数据和方法
var app = angular.module("Utility", []);

app.value("pattern", {
    phone: /^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/,
    Identity: /^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/
});


app.factory("utility", function () {
    return {
        XmlStrToJson: function (xmlStr) {
            var xml = this.XmlStrToXmlObj(xmlStr);
            return this.XmlToJson(xml);
        },
        XmlToJson: function (xml) {
            var obj = {};
            if (xml.nodeType == 1) { // element
                // do attributes
                if (xml.attributes.length > 0) {
                    obj["@attributes"] = {};
                    for (var j = 0; j < xml.attributes.length; j++) {
                        var attribute = xml.attributes.item(j);
                        obj["@attributes"][attribute.nodeName] = attribute.nodeValue;
                    }
                }
            } else if (xml.nodeType == 3) { // text
                obj = xml.nodeValue;
            }

            // do children
            if (xml.hasChildNodes()) {
                for (var i = 0; i < xml.childNodes.length; i++) {
                    var item = xml.childNodes.item(i);
                    var nodeName = item.nodeName;
                    if (typeof (obj[nodeName]) == "undefined") {
                        obj[nodeName] = this.XmlToJson(item);
                    } else {
                        if (typeof (obj[nodeName].length) == "undefined") {
                            var old = obj[nodeName];
                            obj[nodeName] = [];
                            obj[nodeName].push(old);
                        }
                        obj[nodeName].push(this.XmlToJson(item));
                    }
                }
            }
            return obj;
        },
        JsonToXmlStr: function () { },
        XmlStrToXmlObj: function (xmlString) {
            var xmlDoc = null;
            //判断浏览器的类型
            //支持IE浏览器 
            if (!window.DOMParser && window.ActiveXObject) {   //window.DOMParser 判断是否是非ie浏览器
                var xmlDomVersions = ['MSXML.2.DOMDocument.6.0', 'MSXML.2.DOMDocument.3.0', 'Microsoft.XMLDOM'];
                for (var i = 0; i < xmlDomVersions.length; i++) {
                    try {
                        xmlDoc = new ActiveXObject(xmlDomVersions[i]);
                        xmlDoc.async = false;
                        xmlDoc.loadXML(xmlString); //loadXML方法载入xml字符串
                        break;
                    } catch (e) {
                    }
                }
            }
                //支持Mozilla浏览器
            else if (window.DOMParser && document.implementation && document.implementation.createDocument) {
                try {
                    /* DOMParser 对象解析 XML 文本并返回一个 XML Document 对象。
                     * 要使用 DOMParser，使用不带参数的构造函数来实例化它，然后调用其 parseFromString() 方法
                     * parseFromString(text, contentType) 参数text:要解析的 XML 标记 参数contentType文本的内容类型
                     * 可能是 "text/xml" 、"application/xml" 或 "application/xhtml+xml" 中的一个。注意，不支持 "text/html"。
                     */
                    domParser = new DOMParser();
                    xmlDoc = domParser.parseFromString(xmlString, 'text/xml');
                } catch (e) {
                }
            }
            else {
                return null;
            }
            return xmlDoc;
        }
    };
});

app.factory("dictionary", function ($rootScope, optionsServices) {
    return {
        dictionary: function (_callback) {
            if (!$rootScope.Dic) {
                var idsArray = {
                    "GB3304-1991": { Name: "Nationality", Desc: "中国各民族名称的罗马字母拼写法和代码" },
                    "GBT2260-2007": { Name: "CensusAddressCode", Desc: "中华人民共和国行政区划代码" },
                    "GBT2261.1-2003": { Name: "Gender", Desc: "个人基本信息分类与代码 第1部分 人的性别代码" },
                    "GBT2659-2000": { Name: "Country", Desc: "世界各国和地区名称代码" },
                    "GBT2261.2-2003": { Name: "MarriageStatus", Desc: "个人基本信息与分类代码 第2部分 婚姻状况代码" },
                    "GBT4658-1984": { Name: "EducationLevel", Desc: "文化程度代码" },
                    "GBT6565-1999": { Name: "OccupationClass", Desc: "职业分类与代码" },
                    "CV02.01.101": { Name: "IDType", Desc: "身份证件类别代码表" },
                    "CV04.50.005": { Name: "BloodType", Desc: "ABO血型代码表" },
                    "CV05.01.038": { Name: "AllergyHistory", Desc: "过敏源代码表" },
                    "CV03.00.301": { Name: "RiskFactors", Desc: "环境危险因素暴露类别代码表" },
                    "CV02.10.005": { Name: "Disease", Desc: "既往常见疾病种类代码表" },
                    "GBT4761-2008": { Name: "FamilyRelation", Desc: "家庭关系代码" },
                    "CV05.10.001": { Name: "DisabilityStatus", Desc: "残疾情况代码表" },
                    "CV08.50.001": { Name: "VeroName", Desc: "疫苗名称代码表" },
                    "DE04.50.010.00": { Name: "RHType", Desc: "Rh血型代码" },
                    //"DE02.10.050.00":{Name: "FamilyRelation", Desc: "免疫接种情况代码" },
                    //"DE05.10.041.00":{Name: "FamilyRelation", Desc: "老年人认知功能粗筛结果代码" },
                    //"DE05.10.040.00":{Name: "FamilyRelation", Desc: "老年人情感状态粗筛结果代码" },
                    //"CV04.10.006":{Name: "FamilyRelation", Desc: "巩膜检查结果代码表" },
                    //"CV04.10.011":{Name: "FamilyRelation", Desc: "淋巴结检查结果代码表" },
                    //"DE04.10.205.00":{Name: "FamilyRelation", Desc: "心律类别代码" },
                    //"DE04.01.021.00":{Name: "FamilyRelation", Desc: "腹痛程度代码" },
                    //"CV04.10.013":{Name: "FamilyRelation", Desc: "肛门指诊检查结果代码表" },
                    //"CV04.50.015":{Name: "FamilyRelation", Desc: "尿实验室定性检测结果代码表" },
                    //"DE04.50.026.00":{Name: "LiverTest", Desc: "肝功能检测结果代码" },
                    //"DE04.50.076.00":{Name: "KidneyTest", Desc: "肾功能检测结果代码" },
                    "CV07.10.003": { Name: "InsuranceType", Desc: "医疗保险类别代码" },
                    "CV07.10.004": { Name: "PayMethod", Desc: "医疗费用结箅方式代码" }
                };
                var ids = "";
                for (var name in idsArray) {
                    ids += "," + name;
                }
                optionsServices.get({ id: ids.substr(1) },
                    function (data) {
                        $rootScope.Dic = {};
                        for (var name in data) {
                            if (idsArray[name]) {
                                $rootScope.Dic[idsArray[name].Name] = data[name];
                            }
                        }
                        _callback($rootScope.Dic);
                    }
                );

            } else {
                _callback($rootScope.Dic);
            }
        }
    }
});

//日期:20150817
//说明:该部分使用angularjs的指令扩展，定义了一些事件指令
//

//ngRepeat执行完成事件
app.directive('onFinishRepeatRender', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngRepeatFinished');
                });
            }
        }
    };
}).directive('onFinishViewRender', function ($rootScope) {//ngView页面渲染完成事件
    return {
        restrict: 'A',
        compile: function (scope, element, attr) {
            $rootScope.$on("$viewContentLoaded", function (evt, current, previous) {
                InitFixedNaviBar();
            });
        }
    }
});

//日期:20150817
//说明:该部分定义了页面中使用相关第三方插件的对应扩展

//日期控件
app.directive('datepicker', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.datepicker({
                dateFormat: 'yyyymmdd',
                autoclose: true,
                language: "zh-CN"
            });
        }
    };
}).directive('multiselect', function () {//多选控件
    return {
        restrict: 'A',
        priority: 2,
        replace: true,
        template: function (tElement, tAttrs) {
            var chirdrens = tElement.children();
            var style = tAttrs.style ? tAttrs.style : "";
            var tStr = '<div class="input-group"><input type="text" class="form-control" id="' + tAttrs.id + '" title="{{' + tAttrs.bind + '}}" multipleselect/><div class="input-group-btn"><button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">请选择<span class="caret"></span></button>'
                + '<ul class="dropdown-menu dropdown-menu-right multiselect-container" style="overflow-y: auto;overflow-x: hidden;' + style + '">';
            for (var i = 0; i < chirdrens.length; i++) {
                tStr += '<li><a tabindex="' + i + '"><label class="checkbox"><input id="' + tElement.attr("name") + i + '" name="' + tElement.attr("name") + '" type="checkbox" value="' + chirdrens[i].value + '"/>' + chirdrens[i].innerText + '</lable></a></li>';
            }
            tStr += '</ul></div></div>';
            return tStr;
        },
        compile: function (scope, element) {
            var curElement = element.$$element;
            var l = curElement.find("ul li").length;
            curElement.find("ul").width(curElement.width()).height(l >= 10 ? 25 * 10 : 25 * l);
            curElement.find("ul li").click(function (e) {
                var curLi = $(this);
                var text = $(this).text();
                var s = curElement.find("input:text").val();
                var curInput = $(this).find("input");
                if (curLi.hasClass("active")) {
                    curInput[0].checked = false;
                    curLi.removeClass("active");
                    s = s.replace(text + ",", "").replace("," + text, "").replace(text, "");
                } else {
                    curInput[0].checked = true;
                    curLi.addClass("active");

                    if (s == "") {
                        s = text;
                    } else {
                        s += "," + text;
                    }
                }
                curElement.find("input:text").val(s);
                return false;
            });
        }
    };
});