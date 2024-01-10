using DomLibrary;
using SchoolBones.Enums;
using SchoolBones.Teacher;

namespace Focus8W.Controllers
{
    public static class CommonMethod
    {
        static TeacherDML oTeacherDML = new TeacherDML();


        public static DoubleIntValue LoadRegUser(int iModuleId = 0, int iSubModule = 0)
        {
            DoubleIntValue oData = null;
            Registration[] arrRegisteredUsers = null;
            if(iModuleId == (int)Modules.Admin)
            {
                iModuleId++;
            }
            //arrRegisteredUsers = oTeacherDML.LoadRegisteredUsers(iModuleId + 1);
            arrRegisteredUsers = oTeacherDML.LoadRegisteredUsers(iModuleId);
            oData = new DoubleIntValue(Id1: iModuleId, Id2: iSubModule, Value: arrRegisteredUsers);
            return oData;
        }
    }
}