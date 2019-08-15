var chartbmenber;
var chart_qs;

$(function () {
    page_center.pageLoad();
    page_center.loadData();
});

$(window).resize(function () {
    chartbmenber.resize();
    chart_qs.resize();
});

var page_center = {
    pageLoad: function () {
        chartbmenber = echarts.init(document.getElementById("chart_b_member"), 'macarons');
        chart_qs = echarts.init(document.getElementById("chart_qs"), 'macarons');
    },
    loadData: function () {
        var bmenberVArr = [];
        var bmenberledata = [];
        bmenberVArr.push({ value: 100, name: '钻卡社员' });
        bmenberVArr.push({ value: 120, name: '金卡社员' });
        bmenberVArr.push({ value: 200, name: 'VIP社员' });
        bmenberVArr.push({ value: 290, name: '普通社员' });
        bmenberledata.push('钻卡社员');
        bmenberledata.push('金卡社员');
        bmenberledata.push('VIP社员');
        bmenberledata.push('普通社员');
        this.optionbmenber.series[0].data = bmenberVArr;
        //this.optionbmenber.legend.data = bmenberledata;
        chartbmenber.clear();
        chartbmenber.setOption(this.optionbmenber);

        option_qs.xAxis[0].data = ['04-01', '04-02', '04-03', '04-04', '04-05', '04-06', '04-07'];
        option_qs.series[0].data = [1, 4, 7, 9, 3, 6, 7];

        option_qs.series[1].data = [2, 8, 6, 6, 4, 9,8];
        option_qs.series[2].data = [2, 8, 6, 6, 4, 9,8];
        option_qs.series[3].data = [2, 8, 6, 6, 4, 9, 8];
        chart_qs.setOption(option_qs);
    },
    opttitle:function (title) {
        return {
            text: title,
            x: 'center',
            textStyle: {
                color: '#666'
            }
        }
    },
    opttoolbox: {
        feature: {
            saveAsImage: {}
        }
    },
    opttooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    optionbmenber:{
        tooltip: this.opttooltip,
        legend: {
            x: 'center',
            y: 'bottom',
            data: []
        },
        series: [
            {
                name: '社员等级',
                type: 'pie',
                radius: '50%',
                center: ['50%', '45%'],
                data: [],
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
    },
};


var option_qs = {
    tooltip: {
        trigger: 'axis'
    },
    grid: {
        top:'25',
        left: '0',
        right: '25',
        bottom: '10',
        containLabel: true
    },
    //legend: {
    //    data: ['普通社员', 'VIP社员', '银卡社员', '钻卡社员'],
    //    top: 0,
    //    right:0
    //},
    xAxis: [
        {
            type: 'category',
            boundaryGap: false,
            data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
            splitLine: {
                show: false
            }
        }
    ],
    yAxis: {
        type: 'value',
        //boundaryGap: [0, '100%'],
        splitLine: {
            show: false
        }
    },
    series: [
        {
            name: '普通社员',
            type: 'line',
            stack: '总量',
            areaStyle: { normal: {} },
            data: [120, 132, 101, 134, 90, 230, 210]
        },
        {
            name: 'VIP社员',
            type: 'line',
            stack: '总量',
            areaStyle: { normal: {} },
            data: [220, 182, 191, 234, 290, 330, 310]
        },
        {
            name: '银卡社员',
            type: 'line',
            stack: '总量',
            areaStyle: { normal: {} },
            data: [150, 232, 201, 154, 190, 330, 410]
        },
        {
            name: '钻卡社员',
            type: 'line',
            stack: '总量',
            areaStyle: { normal: {} },
            data: [320, 332, 301, 334, 390, 330, 320]
        }
    ]
};