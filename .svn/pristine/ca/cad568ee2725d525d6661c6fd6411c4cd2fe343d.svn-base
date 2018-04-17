function updateAthletesBySportGrid() {
    var athletesBySportGridCoachId = 0;
    if ($('#AthletesBySportCoachSelect')[0])
        $('#AthletesBySportCoachSelect')[0].value;
    var athletesBySportGridHolder = $('#stacked-vertical-chart3');
    var athletesBySportGridXAxisLabels = ['Baseball', 'Basketball', 'Football', 'Cricket'];
    var athletesBySportGridChartOptions = {
        xaxis: {
            tickFormatter: athletesBySportGridXAxisLabelGenerator,
            tickSize: 1
        },
        grid: {
            hoverable: true,
            clickable: false,
            borderWidth: 1,
            tickColor: '#eaeaea',
            borderColor: '#eaeaea',
        },
        series: {
            stack: true
        },
        bars: {
            show: true,
            barWidth: .25,
            fill: true,
            align: 'center',
            lineWidth: 0,
            fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
        },
        shadowSize: 0,
        tooltip: true,
        tooltipOpts: {
            content: '%s: %y'
        },
        colors: ['#4B4B7C', '#3EB15B', '#47BCC7', '#C38FBB'],
    }
    function athletesBySportGridXAxisLabelGenerator(x) {
        for (var i = 0; i < athletesBySportGridXAxisLabels.length; i++) {
            var item = athletesBySportGridXAxisLabels[i];
            if (item[0] == x) {
                return item[1];
            }
        }
        return '';
    }
    $.ajax({
        url: "../Coach/GetAthleteBySportData",
        type: "POST",
        async: true,
        data: { coachId: athletesBySportGridCoachId },
        success: function (resp) {
            if (athletesBySportGridHolder.length) {
                if (resp.axis.length == 0) {
                    resp.axis = [];
                }
                athletesBySportGridXAxisLabels = resp.axis;
                if (resp.data.length == 0) {
                    resp.data = [{ data: [], label: "" }];
                }
                $.plot(athletesBySportGridHolder, resp.data, athletesBySportGridChartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
}
$('#AthletesBySportCoachSelect').on('change', function () {
    updateAthletesBySportGrid();
})
updateAthletesBySportGrid();