import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { UserViewModel } from '../shared/services/generated/api.client.generated';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  user: UserViewModel;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.setUserDetails();
    this.authService.userData.subscribe(u => {
      this.user =u;
    });
  }

}
