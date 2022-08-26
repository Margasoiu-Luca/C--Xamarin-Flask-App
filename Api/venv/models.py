from flask import Flask
import flask
from flask_sqlalchemy import SQLAlchemy
from dataclasses import dataclass
from flask_cors import CORS

import os

#The app object that handles the api, and sets a link to the database in the file
app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///sqlite3.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] =  False
app.config['CORS_HEADERS'] = 'Content-Type'

CORS(app)


#The db object that handles anything database related, such as creating tables and handling queries 
#So far the application only has two 
db = SQLAlchemy(app)

#A "User" table
#Note - id is automatically autoincremented, no need to specify that
#Otherwise pretty strait forward


#These lines are used in conjunction with the @dataclass method in order to
#return a json from the api. If not used then it would attempt
#to return an object which is cant be serialized as a json
#leading into errors and the server crashing

@dataclass
class User(db.Model):
    id:int
    username:str
    password:str

    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String(80), unique=True, nullable=False)
    password = db.Column(db.String(120), unique=False, nullable=False)
    reviews = db.relationship('Food',backref='person')

    #Model table
#Has a foreign key as the username of the User table
#Otherwise pretty strait forward
@dataclass
class Food(db.Model):
    id:int
    name:str
    description:str
    vegan:str
    imageLink:str
    calories:int
    user_name:str

    id = db.Column(db.Integer, primary_key=True, autoincrement=False)
    name = db.Column(db.String(200), unique=False, nullable=False)
    description = db.Column(db.String(200), unique=False, nullable=False)
    vegan = db.Column(db.String(5), unique=False, nullable=False)
    imageLink = db.Column(db.String(200), unique=False, nullable=False)
    calories = db.Column(db.Integer, unique=False, nullable=False)
    user_name = db.Column(db.String(80),db.ForeignKey('user.username'))


#db.ForeignKey(user.id)
