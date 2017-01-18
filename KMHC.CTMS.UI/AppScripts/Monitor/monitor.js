var app = angular.module('Monitor', []);

app.controller("UserStatisticsCtrl", ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.Data = [];
    $scope.DateStart = '';
    $scope.DateEnd = '';
    $scope.loading = false;
    $scope.CurrentPage = 1;

    $scope.Search = function () {
        $scope.loading = true;
        $.getJSON("/api/ctms_sys_userregrecord/GetUserStatistics?dtFrom=" + $scope.DateStart + "&dtTo=" + $scope.DateEnd + "&pageIndex=" + $scope.CurrentPage, function (data) {
            $scope.Data = data.Data;
            var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                $scope.CurrentPage = parseInt(curPage);
                $scope.Search();
            });
            if (data.ErrorMsg)
                alert(data.ErrorMsg);
            $scope.$apply(function () {
                $scope.loading = false;
            });
        });
    };

    $scope.Search();

    $scope.SearchCount = function (type) {
        $("#myModal").on('shown.bs.modal', function () {
            $scope.SearchCountDetail(type);
        }).modal('show');
    };

    $scope.SearchCountDetail = function (type) {
        $.getJSON("/api/ctms_sys_userregrecord/GetUserStatisticsCount?dtStart=" + $scope.DateStart + "&dtEnd=" + $scope.DateEnd + "&type=" + type, function (data) {
            if (data.ErrorMsg) {
                alert(data.ErrorMsg);
                return;
            }
            console.log(data);

            var myChart = echarts.init(document.getElementById('main'));

            option = {
                title: {
                    text: data.Data.title,
                    subtext: '',
                    x: 'center',
                    y: 'top',
                    textAlign: 'left'
                },
                tooltip: {
                    trigger: 'axis'
                },
                toolbox: {
                    show: true,
                    feature: {
                        myToolByDay: {//自定义按钮 danielinbiti,这里增加，selfbuttons可以随便取名字    
                            show: true,//是否显示    
                            title: '按日', //鼠标移动上去显示的文字    
                            icon: 'image://./Content/images/day.png', //图标    
                            option: {},
                            onclick: function (option1) {//点击事件,这里的option1是chart的option信息    
                                $scope.SearchCountDetail("DD");
                            }
                        },
                        myToolByWeek: {//自定义按钮 danielinbiti,这里增加，selfbuttons可以随便取名字    
                            show: true,//是否显示    
                            title: '按周', //鼠标移动上去显示的文字    
                            icon: 'image://./Content/images/week.png', //图标    
                            option: {},
                            onclick: function (option1) {//点击事件,这里的option1是chart的option信息    
                                $scope.SearchCountDetail("iw");
                            }
                        },
                        myToolByMonth: {//自定义按钮 danielinbiti,这里增加，selfbuttons可以随便取名字    
                            show: true,//是否显示    
                            title: '按月', //鼠标移动上去显示的文字    
                            icon: 'image://./Content/images/month.png', //图标    
                            option: {},
                            onclick: function (option1) {//点击事件,这里的option1是chart的option信息    
                                $scope.SearchCountDetail("mm");
                            }
                        },
                        myToolByYear: {//自定义按钮 danielinbiti,这里增加，selfbuttons可以随便取名字    
                            show: true,//是否显示    
                            title: '按年', //鼠标移动上去显示的文字    
                            icon: 'image://./Content/images/year.png', //图标    
                            option: {},
                            onclick: function (option1) {//点击事件,这里的option1是chart的option信息    
                                $scope.SearchCountDetail("yyyy");
                            }
                        },
                    }
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: false,
                        data: data.Data.xAxisData
                    }
                ],
                yAxis: [
                    {
                        type: 'value'
                    }
                ],
                series: data.Data.seriesData
            };

            myChart.setOption(option);
        });
    };
}]);

app.controller("ModelSettingCtrl", ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.Data = [];
    $scope.CurrentPage = 1;
    $scope.loading = false;
    $scope.Keyword = "";
    $scope.Type = $routeParams.type || 0;

    $scope.Reset = function () {
        $scope.Keyword = "";
        $scope.Search();
    };

    $scope.Search = function () {
        $.getJSON("api/monitor/GetModelSettingList?pageIndex=" + $scope.CurrentPage + "&kwd=" + $scope.Keyword + "&type=" + $scope.Type, function (data) {
            $scope.Data = data.Data;
            var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                $scope.CurrentPage = parseInt(curPage);
                $scope.Search();
            });
            if (data.ErrorMsg)
                alert(data.ErrorMsg);
            $scope.$apply(function () {
                $scope.loading = false;
            });
        });
    };

    $scope.Search();

    $scope.Edit = function (obj) {
        var flag = true;
        angular.forEach($scope.Data, function (data, index, array) {
            if (data.IsEdit) {
                alert("您尚有未编辑完成项");
                flag = false;
            }
        });
        obj.IsEdit = flag;
    };
    $scope.AddNew = function () {
        var flag = true;
        angular.forEach($scope.Data, function (data, index, array) {
            if (data.IsNew) {
                alert("您尚有未新增完成项");
                flag = false;
            }
        });
        if (flag == false)
            return;
        $scope.Data.push({ IsNew: true, ModelSource: $scope.Type });
    };
    $scope.CancelEdit = function (obj) {
        if (!obj.IsNew) {
            obj.IsEdit = false;
            $scope.Search();
        } else {
            $scope.Data.removeInArr(obj);
        }
    };
    $scope.Delete = function (obj) {
        if (confirm("确定删除该项？")) {
            obj.IsDeleted = 1;
            $http.post('/api/monitor/SaveModelSetting', { Data: obj }).success(function (data) {
                if (data.Data == true) {
                    alert("操作成功");
                    $scope.Search();
                } else {
                    alert(data.ErrorMsg);
                }
            }).error(function (error) {
                KMAlert({
                    msg: '网络异常！',
                    btnMsg: '确定'
                });
            });
        }
    };
    $scope.SaveEdit = function (obj) {
        if (obj.ModelName == "") {
            alert("请输入模块名称");
            return;
        }
        if (obj.ModelCode == "") {
            alert("请输入模块编码");
            return;
        }
        if (confirm("确定保存修改？")) {
            $http.post('/api/monitor/SaveModelSetting', { Data: obj }).success(function (data) {
                if (data.Data == true) {
                    alert("操作成功");
                    $scope.Search();
                } else {
                    alert(data.ErrorMsg);
                }
            }).error(function (error) {
                KMAlert({
                    msg: '网络异常！',
                    btnMsg: '确定'
                });
            });
        }
    };
}]);

app.controller("ActiveStatisticsCtrl", ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.StartDate = '';
    $scope.EndDate = '';
    $scope.ModelStartDate = '';
    $scope.ModelEndDate = '';
    $scope.CurrentPage = 1;
    $scope.Kwd = '';
    $scope.ModalData = [];

    $scope.Search = function () {
        $.getJSON("/api/monitor/GetActiveCount?dtStart=" + $scope.StartDate + "&dtEnd=" + $scope.EndDate, function (data) {
            if (data.ErrorMsg) {
                alert(data.ErrorMsg);
                return;
            }

            var myChart = echarts.init(document.getElementById('main'));

            myChart.on('click', function (params) {
                console.log(params);
                $scope.Kwd = params.seriesName;
                $scope.ModelStartDate = $scope.StartDate;
                $scope.ModelEndDate = $scope.EndDate;
                $("#myModal").on('shown.bs.modal', function () {
                    $scope.SearchDetails();
                }).modal('show');
            });

            option = {
                tooltip: {
                    trigger: 'xAxis',
                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                    },
                    formatter: function (obj) {
                        var result = "";
                        if (obj.length != undefined) {
                            result = obj[0].name;
                            var temp = "";
                            var count = 0;
                            for (var i = 0; i < obj.length; i++) {
                                temp += "<br/><span style='display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:" + obj[i].color + "'></span> " + obj[i].seriesName + "：" + obj[i].value;
                                count += obj[i].value;
                            }
                            result += " 总数：" + count + temp;
                        } else {
                            result = obj.name + "<br/><span style='display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:" + obj.color + "'></span> " + obj.seriesName + "：" + obj.value;
                        }
                        return result;
                    }
                },
                legend: {
                    y:'bottom',
                    data: data.Data.legendData
                },
                grid: {
                    left: '3%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                xAxis: [
                    {
                        type: 'category',
                        data: data.Data.xAxisData
                    }
                ],
                title:{
                    text: data.Data.title,
                    subtext:'',
                    x:'center',
                    y:'top',
                    textAlign:'left'
                },
                yAxis: [
                    {
                        type: 'value'
                    }
                ],
                series: data.Data.seriesData
            };
            myChart.setOption(option);

            $scope.$apply(function () {
                $scope.loading = false;
            });
        });
    };
    $scope.Search();

    $scope.SearchDetails = function () {
        $.getJSON("/api/monitor/GetActiveDetails?dtStart=" + $scope.ModelStartDate + "&dtEnd=" + $scope.ModelEndDate + "&pageIndex=" + $scope.CurrentPage + "&kwd=" + $scope.Kwd, function (data) {
            if (data.ErrorMsg) {
                alert(data.ErrorMsg);
                return;
            }
            $scope.$apply(function () {
                $scope.ModalData = data.Data;
            });
            
            var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                $scope.CurrentPage = parseInt(curPage);
                $scope.$apply(function () {
                    $scope.SearchDetails();
                });
            });
        });
    };
}]);

app.controller("ErrorCountCtrl", ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.CurrentPage = 1;
    $scope.Kwd = "";
    $scope.Data = [];

    $scope.Search = function () {
        $.getJSON("/api/monitor/GetErrorDetail?pageIndex=" + $scope.CurrentPage + "&kwd=" + $scope.Kwd, function (data) {
            if (data.ErrorMsg) {
                alert(data.ErrorMsg);
                return;
            }

            $scope.$apply(function () {
                $scope.Data = data.Data;
            });
        });
    };
    $scope.Search();
}]);

app.controller("ServerMonitorSettingCtrl", ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.Data = [];
    $scope.CurrentPage = 1;
    $scope.loading = false;
    $scope.Keyword = "";

    $scope.Reset = function () {
        $scope.Keyword = "";
        $scope.Search();
    };

    $scope.Search = function () {
        $.getJSON("api/monitor/GetServerMonitorSetting?pageIndex=" + $scope.CurrentPage + "&kwd=" + $scope.Keyword, function (data) {
            if (data.ErrorMsg) {
                alert(data.ErrorMsg);
                return;
            }
            $scope.$apply(function () {
                $scope.Data = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        });
    };

    $scope.Search();

    $scope.Edit = function (obj) {
        var flag = true;
        angular.forEach($scope.Data, function (data, index, array) {
            if (data.IsEdit) {
                alert("您尚有未编辑完成项");
                flag = false;
            }
        });
        obj.IsEdit = flag;
    };
    $scope.AddNew = function () {
        var flag = true;
        angular.forEach($scope.Data, function (data, index, array) {
            if (data.IsNew) {
                alert("您尚有未新增完成项");
                flag = false;
            }
        });
        if (flag == false)
            return;
        $scope.Data.push({ IsNew: true });
    };
    $scope.CancelEdit = function (obj) {
        if (!obj.IsNew) {
            obj.IsEdit = false;
            $scope.Search();
        } else {
            $scope.Data.removeInArr(obj);
        }
    };
    $scope.Delete = function (obj) {
        if (confirm("确定删除对" + obj.IPAddress + "的监控？")) {
            obj.IsDeleted = 1;
            $http.post('/api/monitor/SaveServerMonitorSetting', { Data: obj }).success(function (data) {
                if (data.Data == true) {
                    alert("操作成功");
                    $scope.Search();
                } else {
                    alert(data.ErrorMsg);
                }
            }).error(function (error) {
                KMAlert({
                    msg: '网络异常！',
                    btnMsg: '确定'
                });
            });
        }
    };
    $scope.SaveEdit = function (obj) {
        if (obj.IPAddress == "") {
            alert("请输入IP地址");
            return;
        }
        if (obj.ServerName == "") {
            alert("请输入服务器名称");
            return;
        }
        if (obj.CPUMaxValue == "") {
            alert("请输入CPU告警阀值");
            return;
        }
        if (obj.MemoryMaxValue == "") {
            alert("请输入内存告警阀值");
            return;
        }
        if (obj.DiskMaxValue == "") {
            alert("请输入硬盘告警阀值");
            return;
        }
        if (obj.ContactUserName == "") {
            alert("请输入消息接收者姓名");
            return;
        }
        if ((obj.MobilePhone == "" || obj.MobilePhone == undefined) && (obj.Email == "" || obj.Email == undefined)) {
            alert("请输入手机号码或者Email");
            return;
        }
        if (obj.Email != undefined) {
            if (obj.Email != "") {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!filter.test(obj.Email)) {
                    alert("请输入正确的邮箱");
                    return;
                }
            }
        }
        if (obj.MobilePhone != undefined) {
            if (obj.MobilePhone != "") {
                if (!(/^1[34578]\d{9}$/.test(obj.MobilePhone))) {
                    alert("请输入正确的手机号码");
                    return;
                }
            }
        }

        if (confirm("确定保存修改？")) {
            $http.post('/api/monitor/SaveServerMonitorSetting', { Data: obj }).success(function (data) {
                if (data.Data == true) {
                    alert("操作成功");
                    $scope.Search();
                } else {
                    alert(data.ErrorMsg);
                }
            }).error(function (error) {
                KMAlert({
                    msg: '网络异常！',
                    btnMsg: '确定'
                });
            });
        }
    };
}]);

app.controller("ServerMonitorCtrl", ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.ServerList = [];
    $scope.CurrentIP = "";
    $scope.ServerName = "";
    $scope.Date = "";

    $scope.InitData = function () {
        $http.get('/api/monitor/GetDistinctServerList', {}).success(function (data, status, headers, config) {
            $scope.ServerList = data.Data;
        }).error(function (data, status, headers, config) {
        });
    };
    $scope.InitData();

    $scope.IPChange = function (ip) {
        console.log(ip);
        angular.forEach($scope.ServerList, function (data, index, array) {
            if (data.IPAddress == ip) {
                $scope.ServerName = data.ServerName;
            }
        });
    };

    $scope.Search = function () {
        if ($scope.CurrentIP == "") {
            alert("请选择服务器");
            return;
        }
        if ($scope.Date == "") {
            alert("请选择日期");
            return;
        }

        $http.get('/api/monitor/GetServerInfoCount?ip=' + $scope.CurrentIP + "&dt=" + $scope.Date, {}).success(function (data, status, headers, config) {
            console.log(data);
            if (data.ErrorMsg) {
                alert(data.ErrorMsg);
                return;
            }
            option = {
                tooltip: {
                    trigger: 'axis',
                    formatter: function (obj) {
                        return obj[0].name + "<br/><span style='display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:" + obj[0].color + "'></span> " + obj[0].seriesName + " " + obj[0].value + "%" + "<br/><span style='display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:" + obj[1].color + "'></span> " + obj[1].seriesName + " " + obj[1].value + "%" + "<br/><span style='display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:" + obj[2].color + "'></span> " + obj[2].seriesName + " " + obj[2].value + "%<br/>"
                    }
                },
                legend: {
                    data: data.Data.legendData
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: false,
                        data: data.Data.xAxisData
                    }
                ],

                yAxis: [
                    {
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} %'
                        }
                    }
                ],
                series: data.Data.seriesData
            };
            var myChart = echarts.init(document.getElementById('main'));
            myChart.setOption(option);

        }).error(function (data, status, headers, config) {
        });
    };
}]);

app.controller("ServerAlarmListCtrl", ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.Data = [];
    $scope.CurrentPage = 1;
    $scope.loading = false;
    $scope.Keyword = "";

    $scope.Reset = function () {
        $scope.Keyword = "";
        $scope.Search();
    };

    $scope.Search = function () {
        $.getJSON("api/monitor/GetServerAlarmList?pageIndex=" + $scope.CurrentPage + "&kwd=" + $scope.Keyword, function (data) {
            if (data.ErrorMsg) {
                alert(data.ErrorMsg);
                return;
            }
            $scope.$apply(function () {
                $scope.Data = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        });
    };

    $scope.Search();
}]);