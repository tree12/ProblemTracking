import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserViewModel, ProblemClient, ProblemViewModel, SolveEnum } from '../shared/services/generated/api.client.generated';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public problems: ProblemViewModel[]

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private problemClient: ProblemClient) {

    this.problemClient.getAllProblems().subscribe(x => this.problems=x );
  }
   GetStatusName(solveEnum:SolveEnum){
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

