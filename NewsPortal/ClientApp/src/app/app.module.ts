import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatDividerModule,
    MatTabsModule,
    MatProgressBarModule,
    MatTooltipModule,
    MatIconModule,
    MatCardModule,
    MatSelectModule,
    MatSnackBarModule,
    MatSliderModule,
    MatSortModule,
    MatTableModule

} from '@angular/material';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NewsService } from '../services/news-service';
import { Login } from './login/login.component';
import { EditNewsComponent } from './edit-news/edit-news.component';
import { AuthGuard } from '../services/auth-guard.service';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';

export function tokenGetter() {
    return localStorage.getItem('jwt');
}

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        Login,
        EditNewsComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,

        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatCheckboxModule,
        MatDividerModule,
        MatTabsModule,
        MatProgressBarModule,
        MatTooltipModule,
        MatIconModule,
        MatCardModule,
        MatSelectModule,
        MatSnackBarModule,
        MatSliderModule,
        MatSortModule,
        MatTableModule,


        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'login', component: Login },
            { path: 'edit-news', component: EditNewsComponent, canActivate: [AuthGuard] },
        ]),
        JwtModule.forRoot({
            config: {
                tokenGetter: tokenGetter
            }
        })
    ],
    providers: [NewsService, AuthGuard, JwtHelperService],
    bootstrap: [AppComponent]
})
export class AppModule { }
