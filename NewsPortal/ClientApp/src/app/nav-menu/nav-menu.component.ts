import { Component } from '@angular/core';
import { AuthGuard } from '../../services/auth-guard.service';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    isExpanded = false;

    constructor(private auth: AuthGuard) {

    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }

    isLoggedIn(): boolean {
        return this.auth.getIsLoggedIn();
    }
}
