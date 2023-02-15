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
  constructor(private authService: AuthService, private machineClient: MachineClient, private problemClient: ProblemClient, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.authService.setUserDetails();
    this.authService.userData.subscribe(u => {
      this.user = u;
    });
    this.machineClient.getMachines().subscribe(x => this.machines = x);
  }
  openDialog(machine: MachineViewModel) {
    const dialogRef = this.dialog.open(StepChangeComponent, {
      data: machine,
    });

    dialogRef.afterClosed().subscribe(result => {
      this.alreadySave = false;
      console.log('The dialog was closed');
      let problem: ProblemViewModel = new ProblemViewModel();
      problem.problemName = "Problem from " + this.authService.userData.getValue().userName;
      problem.description = "Problem of machine Id: " + result.data.machineId;
      problem.user = this.authService.userData.getValue();
      problem.active = true;
      this.problemClient.addProblem(problem).subscribe(problemId => {
        problem.id = problemId;
        let problemInvestigateViewModel: ProblemInvestigateViewModel = new ProblemInvestigateViewModel();
        problemInvestigateViewModel.problem = problem;
        problemInvestigateViewModel.solveStatus = SolveEnum.Solved;
        problemInvestigateViewModel.stepToSolved = result.data;

        this.problemClient.addProblemInvestigate(problemInvestigateViewModel).subscribe(id => {
          this.alreadySave = id ? true : false;
        });
      });

      //Save data
    });

  }
    addProblemDialog(machine: MachineViewModel) {
        const dialogRef = this.dialog.open(AddProblemComponent, {
            data: machine,
            width: '400px',
            height: '400px'
        });
    }
}
