var valueToScroll = 0;
var exerciseSetsDS = null;
var currentExerciseSet = null;

function ScrollGrid() {

	if ($("#icnScrolIcon").hasClass("icon-arrow-bold-right")) {
		valueToScroll = valueToScroll + 100;
		$(".dataTables_scrollBody").animate({ scrollLeft: valueToScroll }, { duration: 200 });

	}
	else {
		valueToScroll = valueToScroll - 100;
		$(".dataTables_scrollBody").animate({ scrollLeft: valueToScroll }, { duration: 200 });

	}

}

function NextSet() {
    DBSave('next');
}

function PrevSet() {
    DBSave('prev');
}

function DBSave(mode) {

	exerciseSetsDS.exerciseSets[currentExerciseSetIndex].ActualLoad = $('#txtActualLoad').val();
	exerciseSetsDS.exerciseSets[currentExerciseSetIndex].ActualReps = $('#txtActualReps').val();

	currentExerciseSet = exerciseSetsDS.exerciseSets[currentExerciseSetIndex];
	$.ajax({
		url: '../Athlete/SaveAthleteWorkout',
		type: 'POST',
		dataType: "json",
		contentType: 'application/json',
		data: JSON.stringify(currentExerciseSet),
		success: function (data, textStatus, jqXHR) {
			if (mode == 'prev') {
				if ($("#lnkPrev")[0].disabled) return;
				$("#lnkNext")[0].disabled = false;
				$('#lnkNext').attr('disabled', false);
				currentExerciseSetIndex = currentExerciseSetIndex - 1;
				if (currentExerciseSetIndex <= 0) {
					$("#lnkPrev")[0].disabled = true;
					$('#lnkPrev').attr('disabled', true);
				}
				else {
					$("#lnkPrev")[0].disabled = false;
					$('#lnkPrev').attr('disabled', false);

				}
			}
			else if (mode == 'next') {
				if ($("#lnkNext")[0].disabled) return;
				$("#lnkPrev")[0].disabled = false;
				$('#lnkPrev').attr('disabled', false);
				currentExerciseSetIndex = currentExerciseSetIndex + 1;

				if (currentExerciseSetIndex == (exerciseSetsDS.exerciseSets.length - 1)) {
					$("#lnkNext")[0].disabled = true;
					$('#lnkNext').attr('disabled', true);
				}
				else {
					$("#lnkNext")[0].disabled = false;
					$('#lnkNext').attr('disabled', false);
				}
			}

			BindViewExercise();
		},
		error: function (xhr, ajaxOptions, thrownError) {
			alert(xhr.responseText);
		}
	});
}

function ChangeExercise() {
    currentExerciseSetIndex = Number(document.getElementById("ddlSets").value);
    if (currentExerciseSetIndex == (exerciseSetsDS.exerciseSets.length - 1)) {
        $("#lnkNext")[0].disabled = true;
        $('#lnkNext').attr('disabled', true);
    }
    else {
        $("#lnkNext")[0].disabled = false;
        $('#lnkNext').attr('disabled', false);
    }

    if (currentExerciseSetIndex == 0) {
        $("#lnkPrev")[0].disabled = true;
        $('#lnkPrev').attr('disabled', true);
    }
    else {
        $("#lnkPrev")[0].disabled = false;
        $('#lnkPrev').attr('disabled', false);
    }
    BindViewExercise();
}

function ChangeExerciseNew() {
    var cIndex = Number(document.getElementById("ddlSetsMain").value);
    $.each(exerciseSetsDS.exerciseSets, function (key, value) {
        if (cIndex == value.ExerciseId)
        {
            $("#ddlSetsMain").val(key);
            currentExerciseSetIndex = key;
            return false;
        }
    });
    if (currentExerciseSetIndex == (exerciseSetsDS.exerciseSets.length - 1)) {
        $("#lnkNext")[0].disabled = true;
        $('#lnkNext').attr('disabled', true);
    }
    else {
        $("#lnkNext")[0].disabled = false;
        $('#lnkNext').attr('disabled', false);
    }

    if (currentExerciseSetIndex == 0) {
        $("#lnkPrev")[0].disabled = true;
        $('#lnkPrev').attr('disabled', true);
    }
    else {
        $("#lnkPrev")[0].disabled = false;
        $('#lnkPrev').attr('disabled', false);
    }
    BindViewExercise();
}

function SaveExercise() {
    exerciseSetsDS.exerciseSets[currentExerciseSetIndex].ActualLoad = $('#txtActualLoad').val();
    exerciseSetsDS.exerciseSets[currentExerciseSetIndex].ActualReps = $('#txtActualReps').val();
}

function BindViewExercise() {
    currentExerciseSet = exerciseSetsDS.exerciseSets[currentExerciseSetIndex];
    $('#ddlSets').val(currentExerciseSetIndex);
    $('#ddlSetsMain').val(currentExerciseSet.ExerciseId);
    $('#txtActualLoad').val(currentExerciseSet.ActualLoad == 0 ? currentExerciseSet.TargetLoad : currentExerciseSet.ActualLoad);
    $('#txtActualReps').val(currentExerciseSet.ActualReps == 0 ? currentExerciseSet.TargetReps : currentExerciseSet.ActualReps);
    $('#lblTargetLoad').html(currentExerciseSet.TargetLoad);
    $('#lblTargetReps').html(currentExerciseSet.TargetReps);
    $('#lblTempo').html(currentExerciseSet.Tempo);
    $('#lblRest').html(currentExerciseSet.Rest);
    $('#lbl1RM').html(currentExerciseSet.OneRM);
    $('#lblDynamic1RM').html(currentExerciseSet.Dynamic1RM);
    $('#lblRest').html(currentExerciseSet.Rest);
    if (currentExerciseSetIndex == 0) {
        $("#lnkPrev")[0].disabled = true;
        $('#lnkPrev').attr('disabled', true);
        document.getElementById("lnkPrev").disabled = true;
    }
    else {
        $("#lnkPrev")[0].disabled = false;
        $('#lnkPrev').attr('disabled', false);
        document.getElementById("lnkPrev").disabled = false;
    }
}

function getUrlVars()
{
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for(var i = 0; i < hashes.length; i++)
    {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
