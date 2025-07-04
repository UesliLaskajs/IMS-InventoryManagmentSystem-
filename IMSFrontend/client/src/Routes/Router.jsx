import React from 'react'
import Home from '../pages/Home';
import { Routes, Route } from 'react-router-dom';
const Router=()=> {
  return (
    <Routes>
        <Route path='/' element={<Home/>} >Home</Route>
    </Routes>
  )
}

export default Router