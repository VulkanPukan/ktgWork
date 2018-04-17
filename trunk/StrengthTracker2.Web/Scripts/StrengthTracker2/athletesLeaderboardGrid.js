athletesLeaderboardGrid = {
    configurationButton: $("#athletesLeaderboardConfigurationButton"),
    configurationModal: $("#athletesLeaderboardModal"),
    configurationSave: $("#athletesLeaderboardSave"),

    configurationSport: $("#athletesLeaderboardConfigurationSport"),
    configurationTeam: $("#athletesLeaderboardConfigurationTeam"),
    configurationPosition: $("#athletesLeaderboardConfigurationPosition"),
    configurationGender: $("#athletesLeaderboardConfigurationGender"),
    configurationGrade: $("#athletesLeaderboardConfigurationGrade"),
    configurationAge: $("#athletesLeaderboardConfigurationAge"),

    initGrid: function () {
        athletesLeaderboardGrid.initModal();
        athletesLeaderboardGrid.initSelects();
    },

    initModal: function () {
        athletesLeaderboardGrid.configurationButton.click(function () {
            athletesLeaderboardGrid.configurationModal.modal('show');
            athletesLeaderboardGrid.configurationSave.click(function () {
                athletesLeaderboardGrid.configurationModal.modal('hide');
            });
        });
    },
    initSelects: function () {
        var disabled = true;
        athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationTeam);
        athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationPosition);
        athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGender);
        athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGrade);
        athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationAge);

        athletesLeaderboardGrid.configurationSport.on('change', function () {
            if (athletesLeaderboardGrid.configurationSport[0].value == 0) {
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationTeam);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationPosition);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGender);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGrade);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationAge);
            }
            else {
                athletesLeaderboardGrid.enableSelect(athletesLeaderboardGrid.configurationTeam);
            }
        });
        athletesLeaderboardGrid.configurationTeam.on('change', function () {
            if (athletesLeaderboardGrid.configurationTeam[0].value == 0) {
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationPosition);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGender);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGrade);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationAge);
            }
            else {
                athletesLeaderboardGrid.enableSelect(athletesLeaderboardGrid.configurationPosition);
            }
        });
        athletesLeaderboardGrid.configurationPosition.on('change', function () {
            if (athletesLeaderboardGrid.configurationPosition[0].value == 0) {
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGender);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGrade);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationAge);
            } else {
                athletesLeaderboardGrid.enableSelect(athletesLeaderboardGrid.configurationGender);
            }
        });
        athletesLeaderboardGrid.configurationGender.on('change', function () {
            if (athletesLeaderboardGrid.configurationGender[0].value == 0) {
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGrade);
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationAge);
            } else {
                athletesLeaderboardGrid.enableSelect(athletesLeaderboardGrid.configurationGrade);
                athletesLeaderboardGrid.enableSelect(athletesLeaderboardGrid.configurationAge);
            }
        });
        athletesLeaderboardGrid.configurationGrade.on('change', function () {
            if (athletesLeaderboardGrid.configurationGrade[0].value != 0) {
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationAge);
            } else {
                athletesLeaderboardGrid.enableSelect(athletesLeaderboardGrid.configurationAge);
            }
        });
        athletesLeaderboardGrid.configurationAge.on('change', function () {
            if (athletesLeaderboardGrid.configurationAge[0].value != 0) {
                athletesLeaderboardGrid.disableSelect(athletesLeaderboardGrid.configurationGrade);
            } else {
                athletesLeaderboardGrid.enableSelect(athletesLeaderboardGrid.configurationGrade);
            }
        });
    },
    disableSelect: function (select) {
        var s = select[0];
        s.disabled = true;
        s.value = 0;
    },
    enableSelect: function(select){
        var s = select[0];
        s.disabled = false;
    }
};