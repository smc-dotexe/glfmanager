import { Component } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  signedIn$: BehaviorSubject<boolean | null>;

  constructor(private authService: AuthService) {
    this.signedIn$ = this.authService.signedIn$;
  }

  ngOnInit() {
    this.authService.checkAuth().subscribe(() => {})
  }

  // checkAuth() {
  //   this.authService.checkAuth().subscribe((response) => {
  //     console.log('auth response=', response);
  //   });
  // }

  signOut() {
    this.authService.signOut();
  }
}
