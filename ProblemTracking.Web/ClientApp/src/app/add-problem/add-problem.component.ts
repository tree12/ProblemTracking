import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MachineViewModel, InvestigateStepViewModel, ProblemClient, ProblemViewModel, SolveEnum } from '../shared/services/generated/api.client.generated';

@Component({
  selector: 'app-add-problem',
  templateUrl: './add-problem.component.html',
  styleUrls: ['./add-problem.component.css']
})
export class AddProblemComponent implements OnInit {
    problemForm: FormGroup;
    constructor(@Inject(MAT_DIALOG_DATA) public data: MachineViewModel, private formBuilder: FormBuilder, public problemClient: ProblemClient, public dialogRef: MatDialogRef<AddProblemComponent>) {

    }

    ngOnInit() {
        this.problemForm = this.formBuilder.group({
            topic: ['', Validators.required],
            description: ['', Validators.required]
        });
  }

    submitForm() {
        if (this.problemForm.valid) {
            this.data;
            let problem = new ProblemViewModel();
            problem.machine = this.data;
            problem.problemName = this.problemForm.controls["topic"].value;
            problem.description = this.problemForm.controls["description"].value;
            problem.solveStatus = SolveEnum.Pending; 

            this.problemClient.addProblem(problem).subscribe(x => {
                this.dialogRef.close();
            });
        }
    }

}
