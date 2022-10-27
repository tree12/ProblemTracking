import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, throwError  } from 'rxjs';
import { map, catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
//import { User } from '../models/user';
import { LoginClient, UserViewModel } from '../../shared/services/generated/api.client.generated';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userData = new BehaviorSubject<UserViewModel>(new UserViewModel());

  constructor(private http: HttpClient, private router: Router, private loginClient: LoginClient) {

    if (!localStorage.getItem('authToken'))
      this.logout();

  }

  login(userDetails: UserViewModel) {
    return  this.loginClient.login(userDetails) //this.http.post<any>('/api/login', userDetails)
      .pipe(map(response => {
        var reader = new FileReader();
        var self = this;
        reader.onload = function () {
          //console.log(reader.result);
          var userDetail = JSON.parse(< string > reader.result);
          if (userDetail) {
            localStorage.setItem('authToken', userDetail["token"]);
            self.setUserDetails();
          }

        }
        reader.readAsText(response.data);

        return response;
      }));

  }

  setUserDetails() {
    if (localStorage.getItem('authToken')) {
      const userDetails = new UserViewModel();
      const decodeUserDetails = JSON.parse(window.atob(localStorage.getItem('authToken').split('.')[1]));

      userDetails.userName = decodeUserDetails.sub;
      userDetails.firstName = decodeUserDetails.firstName;
      userDetails.role = decodeUserDetails.role;

      this.userData.next(userDetails);
    }
  }

  logout() {
    localStorage.removeItem('authToken');
      this.router.navigate(['/login']);
    this.userData.next(new UserViewModel());
    

  }

}
