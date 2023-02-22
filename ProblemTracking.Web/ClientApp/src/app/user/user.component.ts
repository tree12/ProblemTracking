import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { UserViewModel, MachineClient, MachineViewModel, ProblemClient, ProblemViewModel, ProblemInvestigateViewModel, SolveEnum } from '../shared/services/generated/api.client.generated';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { StepChangeComponent } from '../step-change/step-change.component';
import { AddProblemComponent } from '../add-problem/add-problem.component';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

    user: UserViewModel;
    machines: MachineViewModel[];
    problems: ProblemViewModel[];
    alreadySave: boolean = false;
    selectedProblem: ProblemViewModel;
    constructor(private authService: AuthService, private machineClient: MachineClient, private problemClient: ProblemClient, public dialog: MatDialog) { }

    ngOnInit(): void {
        this.authService.setUserDetails();
        this.authService.userData.subscribe(u => {
            this.user = u;
        });
        this.problemClient.getProblemsByUser(this.authService.userData.value.userName).subscribe(x => { this.problems = x; });
        this.machineClient.getMachines().subscribe(x => this.machines = x);
    }
    openDialog(problem: ProblemViewModel) {
        this.selectedProblem = problem;
        const dialogRef = this.dialog.open(StepChangeComponent, {
            data: problem.machine,
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {

                this.alreadySave = false;
                console.log('The dialog was closed');
                let problemInvestigateViewModel: ProblemInvestigateViewModel = new ProblemInvestigateViewModel();
                problemInvestigateViewModel.problem = this.selectedProblem;
                problemInvestigateViewModel.solveStatus = SolveEnum.Solved;
                problemInvestigateViewModel.stepToSolved = result.data;
                //let problem: ProblemViewModel = new ProblemViewModel();
                //problem.problemName = "Problem from " + this.authService.userData.getValue().userName;
                //problem.description = "Problem of machine Id: " + result.data.machineId;
                //problem.user = this.authService.userData.getValue();
                //problem.active = true;
                //this.problemClient.addProblem(problem).subscribe(problemId => {
                //  problem.id = problemId;
                //  let problemInvestigateViewModel: ProblemInvestigateViewModel = new ProblemInvestigateViewModel();
                //  problemInvestigateViewModel.problem = problem;
                //  problemInvestigateViewModel.solveStatus = SolveEnum.Solved;
                //  problemInvestigateViewModel.stepToSolved = result.data;

                this.problemClient.addProblemInvestigate(problemInvestigateViewModel).subscribe(id => {
                    if (id) {
                        this.alreadySave = true;
                        this.problemClient.getProblemsByUser(this.authService.userData.value.userName).subscribe(x => { this.problems = x; });
                    } else {
                        this.alreadySave = false;
                    }

                });
            //});

            //Save data
            }
       
        });

    }
    addProblemDialog(machine: MachineViewModel) {
        const dialogRef = this.dialog.open(AddProblemComponent, {
            data: machine,
            width: '400px',
            height: '400px'
        });

        dialogRef.afterClosed().subscribe(x => {
            this.problemClient.getProblemsByUser(this.authService.userData.value.userName).subscribe(x => { this.problems = x; });
        });
    }
    GetStatusName(solveEnum: SolveEnum) {
        let statusText = "";
        if (solveEnum == SolveEnum.Solved)
            statusText = "Solved";
        else if (solveEnum == SolveEnum.NotSolve)
            statusText = "Not Solved";
        else if (solveEnum == SolveEnum.Pending)
            statusText = "Pending";

        return statusText;
    }
}
