import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-problem',
  templateUrl: './view-problem.component.html',
  styleUrls: ['./view-problem.component.css']
})
export class ViewProblemComponent implements OnInit {

  @Input() userName: string;
  constructor() { }

  ngOnInit(): void {
  }

}
