var currentSet = 1;
var maxSets = 0;
var table = null;
var lb = 5;
var ub = 12;
var column_data = null;
var table_data = null;

function BindWorkoutGrid(data) {
	table_data = JSON.parse(data.Data);
	column_data = JSON.parse(data.ColumnData);
	$('#lblUserName').html(data.UserName);
	table = $('#example').DataTable({
		data: table_data,
		scrollY: "200px",
		scrollX: true,
		scrollCollapse: true,
		paging: false,
		ordering: false,
		searching: false,
		info: false,
		fixedColumns: true,
		fixedColumns: {
			leftColumns: 2
		},
		"columns": column_data,
		'columnDefs': [{
			'targets': 0,
			'searchable': false,
			'orderable': false
		}, {
			'targets': 1,
			'searchable': false,
			'orderable': false
		}, {
			'targets': 2,
			'searchable': false,
			'orderable': false,
			'render': function (data, type, row, full, meta) {
				if (data) {
					return '<span name="id[]" style="color:green;text-align:center; display:block;" class="icon-circle-check 5x"></span>';
				}
				else {
					return '<span name="id[]"></span>';
				}
			}
		}],
		headerCallback: function headerCallback(thead, data, start, end, display) {

		},
		drawCallback: function (settings) {
			$('.dataTables_empty').html("");
		}
	});
	maxSets = Math.max.apply(Math, table_data.map(function (o) { return o.Sets; }));
	if (maxSets >= 1) {
		$('#divSetNav').html("Set 1 of " + maxSets);
		$('.icon-chevron-right').show();
	}
	else {
		$('#divSetNav').html("");
		$('.icon-chevron-right').hide();
	}
	$.each(column_data, function (i, val) {
		if (i > 12) {
			table.columns(i).visible(false);
		}
		else {
			table.columns(i).visible(true);
			table.columns(5).visible(false);
		}
	});
}

function GetAthleteWorkoutDetails() {
	$.ajax({
	    url: '../Athlete/GetAthleteWorkoutDetails',
	    cache:false,
	    success: function (data, textStatus, jqXHR) {
	    	if (data.WorkoutStatusCode == 3) {
	    	   $('#mfSessionComplete').modal('show');
	    	}
	    	else if (data.WorkoutStatusCode == 2) {
	    		$('#mfExerciseNotFound').modal('show');
	    	}
	    	else if (data.WorkoutStatusCode == 4) {
	    	    $('#assessmentStepModal').modal('show');
	    	}
	    	else {
	    	    BindWorkoutGrid(data);
	    	    $("#loading-wrapper").hide();
	    	}
		}
	});
}



function NavigateSets() {

	currentSet = currentSet + 1;
	$('#divSetNav').html("Set " + currentSet + " of " + maxSets)
	if (currentSet == 1) {
		lb = 6;
		ub = 12;
	}
	else {
		lb = ub + 1;
		ub = lb + 7;
	}

	$.each(column_data, function (i, val) {
		
		if ((i >= lb && i <= ub) || i < 6) {
			if (val.title == "ExerciseTempoId" || val.title == "ProgramExerciseId" ) {
				table.columns(i).visible(false);
			}
			else {
				table.columns(i).visible(true);
			}
		}
		else {
			table.columns(i).visible(false);
		}
		
	});

	if (currentSet == maxSets) {
		currentSet = 0;
		lb = 6;
		ub = 12;
	}
}