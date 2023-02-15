import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MachineViewModel, InvestigateStepViewModel, ProblemClient, ProblemInvestigateViewModel } from '../shared/services/generated/api.client.generated';

@Component({
  selector: 'app-step-change',
  templateUrl: './step-change.component.html',
  styleUrls: ['./step-change.component.css']
})
export class StepChangeComponent implements OnInit {
  investigateSteps: InvestigateStepViewModel[];
  selectInvestigateStep: InvestigateStepViewModel;
  machineName: string;
    constructor(@Inject(MAT_DIALOG_DATA) public data: MachineViewModel, public problemClient: ProblemClient, public dialogRef: MatDialogRef<StepChangeComponent>) { }

  ngOnInit(): void {
    if (this.data) {
      this.machineName = this.data.machineName;
      this.investigateSteps = this.data.investigateSteps;
      this.investigateSteps.sort(function (a, b) {
        return a.order - b.order;
      });
    }
      
  }
  checkValue(event, investigateStep) {
    //let value = event.currentTarget.checked;
      this.selectInvestigateStep = investigateStep;
      //let problemInvestigate = new ProblemInvestigateViewModel();
      //problemInvestigate.problem = this.selectInvestigateStep;
      //this.problemClient.addProblemInvestigate(problemInvestigate).subscribe();
  }

  onCloseClick() { this.dialogRef.close({ data: this.selectInvestigateStep }); }

}
