import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';

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
import { NewsPostComponent } from './news-post/news-post.component';0
import { SearchComponent } from './search/search.component';
import { NewsSectionComponent } from './news-section/news-section.component';
import { NewsEditorComponent } from './news-editor/news-editor.component';
import { CategoryEditorComponent } from './category-editor/category-editor.component';
import { CategoryComponent } from './category/category.component';
import { Login } from './login/login.component';
import { CategoryService } from '../services/category.service';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { EditDialogComponent } from './edit-dialog/edit-dialog.component';
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
        NewsPostComponent,
        SearchComponent,
        NewsSectionComponent,
        NewsEditorComponent,
        CategoryEditorComponent,
        CategoryComponent,
        Login,
        DeleteDialogComponent,
        EditDialogComponent,
        EditNewsComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
        MatIconModule,
        MatCardModule,
        MatListModule,
        MatDividerModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonModule,
        MatTableModule,
        MatDialogModule,
        MatSelectModule,
        MatSnackBarModule,
        MatSliderModule,
        MatSortModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'post/:id', component: NewsPostComponent },
            { path: 'search', component: SearchComponent },
            { path: 'category', component: CategoryComponent },
            { path: 'category-editor', component: CategoryEditorComponent },
            { path: 'news-editor', component: NewsEditorComponent },
            { path: 'login', component: Login },
            { path: 'edit-news', component: EditNewsComponent, canActivate: [AuthGuard] },
        ])
    ],
    providers: [NewsService, CategoryService, AuthGuard, JwtHelperService],
    bootstrap: [AppComponent],
    entryComponents: [
        EditDialogComponent,
        DeleteDialogComponent,
    ],
})
export class AppModule { }
