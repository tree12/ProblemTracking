import { Component } from '@angular/core';
import { UserViewModel } from '../shared/services/generated/api.client.generated';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  user: UserViewModel;
  constructor(private authService: AuthService) {
    this.authService.userData.subscribe(x =>this.user=x);
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    this.authService.logout();
  }
}
