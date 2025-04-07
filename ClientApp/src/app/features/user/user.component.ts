import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { UserDetailsVm } from './model/user-details.model';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-management',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  imports: [
    CommonModule,
    FormsModule,
  ],
  providers: [DatePipe] 
})
export class UserManagementComponent implements OnInit {
  users: UserDetailsVm[] = [];
  selectedUser: UserDetailsVm | null = null;
  lockDuration: string = '24h';
  isModalVisible: boolean = false;
  currentPage: number = 1;
  pageSize: number = 3; // Số lượng người dùng mỗi trang
  totalPages: number = 2;
  pagedUsers: UserDetailsVm[] = []; // Người dùng được phân trang

  constructor(private userService: UserService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe((data) => {
      this.users = data.map(user => {
        user.lockoutEnd = this.datePipe.transform(user.lockoutEnd, 'yyyy-MM-dd HH:mm:ss');
        return user;
      });
      this.totalPages = Math.ceil(this.users.length / this.pageSize); // Tính tổng số trang
      this.updatePagedUsers(); // Cập nhật danh sách người dùng theo trang
    }, (error) => {
      console.error('Error fetching users', error);
    });
  }

  // Hàm cập nhật danh sách người dùng của trang hiện tại
  updatePagedUsers(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.pagedUsers = this.users.slice(startIndex, endIndex);
  }

  // Thay đổi trang hiện tại
  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePagedUsers();
    }
  }

  // Hiển thị chi tiết người dùng khi click
  selectUser(user: UserDetailsVm): void {
    this.selectedUser = user;
    this.loadUsers();
    if (this.selectedUser) {
      this.selectedUser.lockoutEnd = this.datePipe.transform(this.selectedUser.lockoutEnd, 'yyyy-MM-dd HH:mm:ss');

    }
  }

  lockAccount(userId: number): void {
    if (!this.lockDuration) {
      alert('Please select a duration for locking the account.');
      return;
    }

    const user = this.users.find(u => u.userId === userId);
    if (user && user.username === 'admin') {
      alert('Không thể khóa tài khoản admin!');
      return;
    }
    if (user && user.lockoutEnd && new Date(user.lockoutEnd) > new Date()) {
      alert('This account is already locked.');
      return;
      
    }

    this.userService.lockUserAccount(userId, this.lockDuration).subscribe((response) => {
      alert('User account locked successfully');
      this.loadUsers(); // Reload user list
    }, (error) => {
      console.error('Error locking user account:', error);
      alert('There was an error locking the user account.');
    });
  }

  unlockAccount(userId: number): void {
    const user = this.users.find(u => u.userId === userId);
    // if (user && user.lockoutEnd && new Date(user.lockoutEnd) > new Date()) {
    //   alert('This account is still locked and cannot be unlocked yet.');
    //   return;
    // }

    this.userService.unlockUserAccount(userId).subscribe((response) => {
      alert('User account unlocked successfully');
      this.loadUsers(); // Reload user list
    }, (error) => {
      console.error('Error unlocking user account', error);
      alert('There was an error unlocking the user account.');
    });
  }
}
