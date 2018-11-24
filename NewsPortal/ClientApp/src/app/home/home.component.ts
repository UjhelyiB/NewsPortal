import { Component } from '@angular/core';
import { AuthGuard } from '../../services/auth-guard.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

    constructor(private auth: AuthGuard) {

    }

    isLoggedIn() {
        return this.auth.getIsLoggedIn();
    }

    logOut() {
        this.auth.logOut();
    }
}
