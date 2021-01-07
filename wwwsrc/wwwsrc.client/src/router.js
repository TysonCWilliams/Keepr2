import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard } from '@bcwdev/auth0provider-client'
function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
  {
    path: '/about',
    name: 'AboutPage',
    component: loadPage('AboutPage'),
    beforeEnter: authGuard
  },
  {
    path: '/api/profiles',
    name: 'Profile',
    component: loadPage('ProfilePage'),
    beforeEnter: authGuard
  },
  // {
  //   path: '/keepDetails/:keepId',
  //   name: 'KeepDetailsPage',
  //   component: loadPage('KeepDetailsPage')
  // },
  {
    path: '/vaultDetails/:vaultId',
    name: 'VaultDetailsPage',
    component: loadPage('VaultDetailsPage')
  },
  {
    path: '/users/:userId',
    name: 'UserPage',
    component: loadPage('UserPage')
  }
]

const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})

export default router
