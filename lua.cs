//lua interface for c#
//by lxd

/* 
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
    [DllImport("libname")]
    public static extern bool func(int arg);
#elif UNITY_IPHONE
    [DllImport (libname)]
    public static extern bool func(int arg);
#elif UNITY_ANDROID
    [DllImport("libname")]
    public static extern bool func(int arg);
#endif
*/

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class lua
{

    // || UNITY_EDITOR
#if UNITY_STANDALONE_WIN
    private const string libname = "liblua";
#elif UNITY_STANDALONE_OSX
    private const string libname = "liblua_mac";
#elif UNITY_IPHONE
    private const string libname = "__Internal";
#elif UNITY_ANDROID
	private const string libname = "liblua";
#endif

    public const string LUA_VERSION_MAJOR = "5";
    public const string LUA_VERSION_MINOR = "2";
    public const int LUA_VERSION_NUM = 502;
    public const string LUA_VERSION_RELEASE = "3";

    //luaconf.h
    public class luaconf
    {
        public const int LUA_IDSIZE = 60;
        public const int LUAI_MAXSTACK = 15000;
        public const int LUAI_FIRSTPSEUDOIDX = -LUAI_MAXSTACK - 1000;
    }

    //lua.h
    /* option for multiple returns in `lua_pcall' and `lua_call' */
    public const int LUA_MULTRET = -1;

    /*
    ** pseudo-indices
    */
    public const int LUA_REGISTRYINDEX = luaconf.LUAI_FIRSTPSEUDOIDX;
    public static int lua_upvalueindex(int i) { return LUA_REGISTRYINDEX - i; }

    /* thread status; 0 is OK */
    public const int LUA_OK = 0;
    public const int LUA_YIELD = 1;
    public const int LUA_ERRRUN = 2;
    public const int LUA_ERRSYNTAX = 3;
    public const int LUA_ERRMEM = 4;
    public const int LUA_ERRGCMM = 5;
    public const int LUA_ERRERR = 6;

    public delegate int lua_CFunction(IntPtr L);

    /*
    ** functions that read/write blocks when loading/dumping Lua chunks
    */
    public delegate /*string*/IntPtr lua_Reader(IntPtr L, IntPtr ud, out int sz);
    public delegate int lua_Writer(IntPtr L, IntPtr p, int sz, IntPtr ud);

    /*
    ** prototype for memory-allocation functions
    */
    public delegate IntPtr lua_Alloc(IntPtr ud, IntPtr ptr, int osize, int nsize);

    /*
    ** basic types
    */
    public const int LUA_TNONE = -1;

    public const int LUA_TNIL = 0;
    public const int LUA_TBOOLEAN = 1;
    public const int LUA_TLIGHTUSERDATA = 2;
    public const int LUA_TNUMBER = 3;
    public const int LUA_TSTRING = 4;
    public const int LUA_TTABLE = 5;
    public const int LUA_TFUNCTION = 6;
    public const int LUA_TUSERDATA = 7;
    public const int LUA_TTHREAD = 8;

    public const int LUA_NUMTAGS = 9;

    /* minimum Lua stack available to a C function */
    public const int LUA_MINSTACK = 20;

    /* predefined values in the registry */
    public const int LUA_RIDX_MAINTHREAD = 1;
    public const int LUA_RIDX_GLOBALS = 2;
    public const int LUA_RIDX_LAST = LUA_RIDX_GLOBALS;

    [DllImport(libname)]
    public static extern IntPtr lua_newstate(lua_Alloc f, IntPtr ud);

    [DllImport(libname)]
    public static extern void lua_close(IntPtr L);

    [DllImport(libname)]
    public static extern IntPtr lua_newthread(IntPtr L);

    [DllImport(libname)]
    public static extern lua_CFunction lua_atpanic(IntPtr L, lua_CFunction panicf);

    /*
    ** basic stack manipulation
    */
    [DllImport(libname)]
    public static extern void lua_absindex(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern int lua_gettop(IntPtr L);
    [DllImport(libname)]
    public static extern void lua_settop(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_pushvalue(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_remove(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_insert(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_replace(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_copy(IntPtr L, int fromidx, int toidx);
    [DllImport(libname)]
    public static extern int lua_checkstack(IntPtr L, int sz);

    [DllImport(libname)]
    public static extern void lua_xmove(IntPtr from, IntPtr to, int n);

    /*
    ** access functions (stack -> C)
    */
    [DllImport(libname)]
    public static extern int lua_isnumber(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern int lua_isstring(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern int lua_iscfunction(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern int lua_isuserdata(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern int lua_type(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern /*string*/IntPtr lua_typename(IntPtr L, int tp);

    //[DllImport(libname)]
    //public static extern int lua_equal(IntPtr L, int idx1, int idx2);
    //[DllImport(libname)]
    //public static extern int lua_rawequal(IntPtr L, int idx1, int idx2);
    //[DllImport(libname)]
    //public static extern int lua_lessthan(IntPtr L, int idx1, int idx2);

    [DllImport(libname)]
    public static extern double lua_tonumberx(IntPtr L, int idx, out int isnum);
    [DllImport(libname)]
    public static extern int lua_tointegerx(IntPtr L, int idx, out int isnum);
    [DllImport(libname)]
    public static extern uint lua_tounsignedx(IntPtr L, int idx, out int isnum);
    [DllImport(libname)]
    public static extern int lua_toboolean(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern /*string*/IntPtr lua_tolstring(IntPtr L, int idx, out int len);
    [DllImport(libname)]
    public static extern int lua_rawlen(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern lua_CFunction lua_tocfunction(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern IntPtr lua_touserdata(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern IntPtr lua_tothread(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern IntPtr lua_topointer(IntPtr L, int idx);

    /*
    ** Comparison and arithmetic functions
    */
    public const int LUA_OPADD = 0;  /* ORDER TM */
    public const int LUA_OPSUB = 1;
    public const int LUA_OPMUL = 2;
    public const int LUA_OPDIV = 3;
    public const int LUA_OPMOD = 4;
    public const int LUA_OPPOW = 5;
    public const int LUA_OPUNM = 6;

    [DllImport(libname)]
    public static extern void lua_arith(IntPtr L, int op);

    public const int LUA_OPEQ = 0;
    public const int LUA_OPLT = 1;
    public const int LUA_OPLE = 2;

    [DllImport(libname)]
    public static extern int lua_rawequal(IntPtr L, int idx1, int idx2);
    [DllImport(libname)]
    public static extern int lua_compare(IntPtr L, int idx1, int idx2, int op);

    /*
    ** push functions (C -> stack)
    */
    [DllImport(libname)]
    public static extern void lua_pushnil(IntPtr L);
    [DllImport(libname)]
    public static extern void lua_pushnumber(IntPtr L, double n);
    [DllImport(libname)]
    public static extern void lua_pushinteger(IntPtr L, int n);
    [DllImport(libname)]
    public static extern void lua_pushunsigned(IntPtr L, uint n);
    [DllImport(libname)]
    public static extern void lua_pushlstring(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string s, int l);
    [DllImport(libname)]
    public static extern void lua_pushstring(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string s);
    //public static extern string lua_pushvfstring(IntPtr L, string fmt, va_list argp);
    //public static extern string lua_pushfstring(IntPtr L, string fmt, ...);
    [DllImport(libname)]
    public static extern void lua_pushcclosure(IntPtr L, lua_CFunction fn, int n);
    [DllImport(libname)]
    public static extern void lua_pushboolean(IntPtr L, int b);
    [DllImport(libname)]
    public static extern void lua_pushlightuserdata(IntPtr L, IntPtr p);
    [DllImport(libname)]
    public static extern int lua_pushthread(IntPtr L);

    /*
    ** get functions (Lua -> stack)
    */
    [DllImport(libname)]
    public static extern void lua_getglobal(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string var);
    [DllImport(libname)]
    public static extern void lua_gettable(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_getfield(IntPtr L, int idx, [MarshalAs(UnmanagedType.LPStr)]string k);
    [DllImport(libname)]
    public static extern void lua_rawget(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_rawgeti(IntPtr L, int idx, int n);
    [DllImport(libname)]
    public static extern void lua_rawgetp(IntPtr L, int idx, IntPtr p);
    [DllImport(libname)]
    public static extern void lua_createtable(IntPtr L, int narr, int nrec);
    [DllImport(libname)]
    public static extern IntPtr lua_newuserdata(IntPtr L, int sz);
    [DllImport(libname)]
    public static extern int lua_getmetatable(IntPtr L, int objindex);
    [DllImport(libname)]
    public static extern void lua_getuservalue(IntPtr L, int idx);

    /*
    ** set functions (stack -> Lua)
    */
    [DllImport(libname)]
    public static extern void lua_setglobal(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string var);
    [DllImport(libname)]
    public static extern void lua_settable(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_setfield(IntPtr L, int idx, [MarshalAs(UnmanagedType.LPStr)]string k);
    [DllImport(libname)]
    public static extern void lua_rawset(IntPtr L, int idx);
    [DllImport(libname)]
    public static extern void lua_rawseti(IntPtr L, int idx, int n);
    [DllImport(libname)]
    public static extern void lua_rawsetp(IntPtr L, int idx, IntPtr p);
    [DllImport(libname)]
    public static extern int lua_setmetatable(IntPtr L, int objindex);
    [DllImport(libname)]
    public static extern int lua_setuservalue(IntPtr L, int idx);

    /*
    ** `load' and `call' functions (load and run Lua code)
    */
    [DllImport(libname)]
    public static extern void lua_callk(IntPtr L, int nargs, int nresults, lua_CFunction k);
    public static void lua_call(IntPtr L, int nargs, int nresults)
    {
        lua_callk(L, nargs, nresults, null);
    }

    [DllImport(libname)]
    public static extern int lua_getctx(IntPtr L, out int ctx);
    [DllImport(libname)]
    public static extern int lua_pcallk(IntPtr L, int nargs, int nresults, int errfunc, int ctx, lua_CFunction k);
    public static int lua_pcall(IntPtr L, int nargs, int nresults, int errfunc)
    {
        return lua_pcallk(L, nargs, nresults, errfunc, 0, null);
    }
    //[DllImport(libname)]
    //public static extern int lua_cpcall(IntPtr L, lua_CFunction func, IntPtr ud);
    [DllImport(libname)]
    public static extern int lua_load(IntPtr L, lua_Reader reader, IntPtr dt,
        [MarshalAs(UnmanagedType.LPStr)]string chunkname, [MarshalAs(UnmanagedType.LPStr)]string mode);

    [DllImport(libname)]
    public static extern int lua_dump(IntPtr L, lua_Writer writer, IntPtr data);

    /*
    ** coroutine functions
    */
    [DllImport(libname)]
    public static extern int lua_yieldk(IntPtr L, int nresults, int ctx, lua_CFunction k);
    public static int lua_yield(IntPtr L, int nresults)
    {
        return lua_yieldk(L, nresults, 0, null);
    }
    [DllImport(libname)]
    public static extern int lua_resume(IntPtr L, IntPtr from, int narg);
    [DllImport(libname)]
    public static extern int lua_status(IntPtr L);

    /*
    ** garbage-collection function and options
    */

    public const int LUA_GCSTOP = 0;
    public const int LUA_GCRESTART = 1;
    public const int LUA_GCCOLLECT = 2;
    public const int LUA_GCCOUNT = 3;
    public const int LUA_GCCOUNTB = 4;
    public const int LUA_GCSTEP = 5;
    public const int LUA_GCSETPAUSE = 6;
    public const int LUA_GCSETSTEPMUL = 7;
    public const int LUA_GCSETMAJORINC = 8;
    public const int LUA_GCISRUNNING = 9;
    public const int LUA_GCGEN = 10;
    public const int LUA_GCINC = 11;

    [DllImport(libname)]
    public static extern int lua_gc(IntPtr L, int what, int data);

    /*
    ** miscellaneous functions
    */

    [DllImport(libname)]
    public static extern int lua_error(IntPtr L);

    [DllImport(libname)]
    public static extern int lua_next(IntPtr L, int idx);

    [DllImport(libname)]
    public static extern void lua_concat(IntPtr L, int n);
    [DllImport(libname)]
    public static extern void lua_len(IntPtr L, int idx);

    [DllImport(libname)]
    public static extern lua_Alloc lua_getallocf(IntPtr L, out IntPtr ud);
    [DllImport(libname)]
    public static extern void lua_setallocf(IntPtr L, lua_Alloc f, IntPtr ud);

    /* 
    ** ===============================================================
    ** some useful macros
    ** ===============================================================
    */
    public static double lua_tonumber(IntPtr L, int i)
    {
        int isnum = 0;
        return lua_tonumberx(L, i, out isnum);
    }
    public static int lua_tointeger(IntPtr L, int i)
    {
        int isnum = 0;
        return lua_tointegerx(L, i, out isnum);
    }
    public static uint lua_tounsigned(IntPtr L, int i)
    {
        int isnum = 0;
        return lua_tounsignedx(L, i, out isnum);
    }

    public static void lua_pop(IntPtr L, int n)
    {
        lua_settop(L, -(n) - 1);
    }

    public static void lua_newtable(IntPtr L)
    {
        lua_createtable(L, 0, 0);
    }

    public static void lua_register(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string n, lua_CFunction f)
    {
        lua_pushcfunction(L, f);
        lua_setglobal(L, n);
    }

    public static void lua_pushcfunction(IntPtr L, lua_CFunction f)
    {
        lua_pushcclosure(L, f, 0);
    }

    public static bool lua_isfunction(IntPtr L, int n)
    {
        return lua_type(L, n) == LUA_TFUNCTION;
    }

    public static bool lua_istable(IntPtr L, int n)
    {
        return lua_type(L, n) == LUA_TTABLE;
    }

    public static bool lua_islightuserdata(IntPtr L, int n)
    {
        return lua_type(L, n) == LUA_TLIGHTUSERDATA;
    }

    public static bool lua_isnil(IntPtr L, int n)
    {
        return lua_type(L, n) == LUA_TNIL;
    }

    public static bool lua_isboolean(IntPtr L, int n)
    {
        return lua_type(L, n) == LUA_TBOOLEAN;
    }

    public static bool lua_isthread(IntPtr L, int n)
    {
        return lua_type(L, n) == LUA_TTHREAD;
    }

    public static bool lua_isnone(IntPtr L, int n)
    {
        return lua_type(L, n) == LUA_TNONE;
    }

    public static bool lua_isnoneornil(IntPtr L, int n)
    {
        return lua_type(L, n) <= 0;
    }

    public static void lua_pushliteral(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string s)
    {
        //TODO: Implement use using lua_pushlstring instead of lua_pushstring
        //lua_pushlstring(L, "" s, (sizeof(s)/GetUnmanagedSize(typeof(char)))-1)
        lua_pushstring(L, s);
    }

    public static void lua_pushglobaltable(IntPtr L)
    {
        lua_rawgeti(L, LUA_REGISTRYINDEX, LUA_RIDX_GLOBALS);
    }

    public static string lua_tostring(IntPtr L, int i)
    {
        int len = 0;
        IntPtr p = lua_tolstring(L, i, out len);
        return Marshal.PtrToStringAnsi(p, len);
    }

    /*
    ** {======================================================================
    ** Debug API
    ** =======================================================================
    */

    /*
    ** Event codes
    */
    public const int LUA_HOOKCALL = 0;
    public const int LUA_HOOKRET = 1;
    public const int LUA_HOOKLINE = 2;
    public const int LUA_HOOKCOUNT = 3;
    public const int LUA_HOOKTAILRET = 4;

    /*
    ** Event masks
    */
    public const int LUA_MASKCALL = (1 << LUA_HOOKCALL);
    public const int LUA_MASKRET = (1 << LUA_HOOKRET);
    public const int LUA_MASKLINE = (1 << LUA_HOOKLINE);
    public const int LUA_MASKCOUNT = (1 << LUA_HOOKCOUNT);

    [StructLayout(LayoutKind.Sequential)]
    public struct lua_Debug
    {
        public int event_;
        [MarshalAs(UnmanagedType.LPStr)]
        public string name;     /* (n) */
        [MarshalAs(UnmanagedType.LPStr)]
        public string namewhat; /* (n) `global', `local', `field', `method' */
        [MarshalAs(UnmanagedType.LPStr)]
        public string what;     /* (S) `Lua', `C', `main', `tail' */
        [MarshalAs(UnmanagedType.LPStr)]
        public string source;   /* (S) */
        public int currentline; /* (l) */
        public int nups;        /* (u) number of upvalues */
        public int linedefined; /* (S) */
        public int lastlinedefined; /* (S) */
        [MarshalAs(UnmanagedType.LPStr, SizeConst = luaconf.LUA_IDSIZE)]
        public string short_src; /* (S) */
        /* private part */
        public int i_ci;  /* active function */
    };

    /* Functions to be called by the debuger in specific events */
    public delegate void lua_Hook(IntPtr L, ref lua_Debug ar);

    [DllImport(libname)]
    public static extern int lua_getstack(IntPtr L, int level, lua_Debug ar);
    [DllImport(libname)]
    public static extern int lua_getinfo(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string what, lua_Debug ar);
    [DllImport(libname)]
    public static extern /*string*/IntPtr lua_getlocal(IntPtr L, lua_Debug ar, int n);
    [DllImport(libname)]
    public static extern /*string*/IntPtr lua_setlocal(IntPtr L, lua_Debug ar, int n);
    [DllImport(libname)]
    public static extern /*string*/IntPtr lua_getupvalue(IntPtr L, int funcindex, int n);
    [DllImport(libname)]
    public static extern /*string*/IntPtr lua_setupvalue(IntPtr L, int funcindex, int n);

    [DllImport(libname)]
    public static extern IntPtr lua_upvalueid(IntPtr L, int fidx, int n);
    [DllImport(libname)]
    public static extern void lua_upvaluejoin(IntPtr L, int fidx1, int n1, int fidx2, int n2);

    [DllImport(libname)]
    public static extern int lua_sethook(IntPtr L, lua_Hook func, int mask, int count);
    [DllImport(libname)]
    public static extern lua_Hook lua_gethook(IntPtr L);
    [DllImport(libname)]
    public static extern int lua_gethookmask(IntPtr L);
    [DllImport(libname)]
    public static extern int lua_gethookcount(IntPtr L);

    /* }====================================================================== */


    //lauxlib.h
    /* extra error code for `luaL_load' */
    public const int LUA_ERRFILE = (LUA_ERRERR + 1);

    [StructLayout(LayoutKind.Sequential)]
    public struct luaL_Reg
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string name;
        public lua_CFunction func;
    };

    //[DllImport(libname)]
    //public static extern void luaL_openlib(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string libname, luaL_Reg[] l, int nup);
    //[DllImport(libname)]
    //public static extern void luaL_register(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string libname, luaL_Reg[] l);
    [DllImport(libname)]
    public static extern int luaL_getmetafield(IntPtr L, int obj, [MarshalAs(UnmanagedType.LPStr)]string e);
    [DllImport(libname)]
    public static extern int luaL_callmeta(IntPtr L, int obj, [MarshalAs(UnmanagedType.LPStr)]string e);
    [DllImport(libname)]
    public static extern /*string*/IntPtr luaL_tolstring(IntPtr L, int idx, out int len);
    [DllImport(libname)]
    public static extern int luaL_argerror(IntPtr L, int numarg, [MarshalAs(UnmanagedType.LPStr)]string extramsg);
    [DllImport(libname)]
    public static extern /*string*/IntPtr luaL_checklstring(IntPtr L, int numArg, out int l);
    [DllImport(libname)]
    public static extern /*string*/IntPtr luaL_optlstring(IntPtr L, int numArg, [MarshalAs(UnmanagedType.LPStr)]string def, out int l);
    [DllImport(libname)]
    public static extern double luaL_checknumber(IntPtr L, int numArg);
    [DllImport(libname)]
    public static extern double luaL_optnumber(IntPtr L, int nArg, double def);

    [DllImport(libname)]
    public static extern int luaL_checkinteger(IntPtr L, int numArg);
    [DllImport(libname)]
    public static extern int luaL_optinteger(IntPtr L, int nArg, int def);

    [DllImport(libname)]
    public static extern uint luaL_checkunsigned(IntPtr L, int numArg);
    [DllImport(libname)]
    public static extern uint luaL_optunsigned(IntPtr L, int nArg, uint def);

    [DllImport(libname)]
    public static extern void luaL_checkstack(IntPtr L, int sz, [MarshalAs(UnmanagedType.LPStr)]string msg);
    [DllImport(libname)]
    public static extern void luaL_checktype(IntPtr L, int narg, int t);
    [DllImport(libname)]
    public static extern void luaL_checkany(IntPtr L, int narg);

    [DllImport(libname)]
    public static extern int luaL_newmetatable(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string tname);
    [DllImport(libname)]
    public static extern void luaL_setmetatable(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string tname);
    [DllImport(libname)]
    public static extern IntPtr luaL_testudata(IntPtr L, int ud, [MarshalAs(UnmanagedType.LPStr)]string tname);
    [DllImport(libname)]
    public static extern IntPtr luaL_checkudata(IntPtr L, int ud, [MarshalAs(UnmanagedType.LPStr)]string tname);

    [DllImport(libname)]
    public static extern void luaL_where(IntPtr L, int lvl);
    //public static extern int luaL_error(IntPtr L, string fmt, ...);

    //public static extern int luaL_checkoption(IntPtr L, int narg, string def, string const lst[]);

    [DllImport(libname)]
    public static extern int luaL_fileresult(IntPtr L, int stat, [MarshalAs(UnmanagedType.LPStr)]string fname);
    [DllImport(libname)]
    public static extern int luaL_execresult(IntPtr L, int stat);

    public static int LUA_NOREF = -2;
    public static int LUA_REFNIL = -2;

    [DllImport(libname)]
    public static extern int luaL_ref(IntPtr L, int t);
    [DllImport(libname)]
    public static extern void luaL_unref(IntPtr L, int t, int nref);

    [DllImport(libname)]
    public static extern int luaL_loadfilex(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string filename, [MarshalAs(UnmanagedType.LPStr)]string mode);
    public static int luaL_loadfile(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string f)
    {
        return luaL_loadfilex(L, f, null);
    }
    [DllImport(libname)]
    public static extern int luaL_loadbufferx(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string buff, int sz,
                                                        [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string mode);
    [DllImport(libname)]
    public static extern int luaL_loadstring(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string s);

    [DllImport(libname)]
    public static extern IntPtr luaL_newstate();

    [DllImport(libname)]
    public static extern int luaL_len(IntPtr L, int idx);

    [DllImport(libname)]
    public static extern /*string*/IntPtr luaL_gsub(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string s, [MarshalAs(UnmanagedType.LPStr)]string p,
                                                              [MarshalAs(UnmanagedType.LPStr)]string r);

    [DllImport(libname)]
    public static extern void luaL_setfuncs(IntPtr L, luaL_Reg[] l, int nup);

    [DllImport(libname)]
    public static extern int luaL_getsubtable(IntPtr L, int idx, [MarshalAs(UnmanagedType.LPStr)]string fname);

    [DllImport(libname)]
    public static extern void luaL_traceback(IntPtr L, IntPtr L1, [MarshalAs(UnmanagedType.LPStr)]string msg, int level);

    [DllImport(libname)]
    public static extern void luaL_requiref(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string msg, lua_CFunction openf, int glb);

    /*
    ** ===============================================================
    ** some useful macros
    ** ===============================================================
    */
    //luaL_newlibtable
    //luaL_newlib

    public static void luaL_argcheck(IntPtr L, bool cond, int numarg, [MarshalAs(UnmanagedType.LPStr)]string extramsg)
    {
        if (!cond)
            luaL_argerror(L, numarg, extramsg);
    }
    public static string luaL_checkstring(IntPtr L, int n)
    {
        int len;
        IntPtr p = luaL_checklstring(L, n, out len);
        return Marshal.PtrToStringAnsi(p, len);
    }
    public static string luaL_optstring(IntPtr L, int n, [MarshalAs(UnmanagedType.LPStr)]string d)
    {
        int len;
        IntPtr p = luaL_optlstring(L, n, d, out len);
        return Marshal.PtrToStringAnsi(p, len);
    }
    public static int luaL_checkint(IntPtr L, int n)
    {
        return (int)luaL_checkinteger(L, n);
    }
    public static int luaL_optint(IntPtr L, int n, int d)
    {
        return (int)luaL_optinteger(L, n, d);
    }
    public static long luaL_checklong(IntPtr L, int n)
    {
        return (long)luaL_checkinteger(L, n);
    }
    public static long luaL_optlong(IntPtr L, int n, int d)
    {
        return (long)luaL_optinteger(L, n, d);
    }

    public static string luaL_typename(IntPtr L, int i)
    {
        IntPtr p = lua_typename(L, lua_type(L, i));
        return Marshal.PtrToStringAnsi(p);
    }

    public static int luaL_dofile(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string s)
    {
        int ret = luaL_loadfile(L, s);
        if (ret != 0)
        {
            return ret;
        }
        return lua_pcall(L, 0, LUA_MULTRET, 0);
    }

    public static int luaL_dostring(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string s)
    {
        int ret = luaL_loadstring(L, s);
        if (ret != 0)
        {
            return ret;
        }
        return lua_pcall(L, 0, LUA_MULTRET, 0);
    }

    public static void luaL_getmetatable(IntPtr L, [MarshalAs(UnmanagedType.LPStr)]string n)
    {
        lua_getfield(L, LUA_REGISTRYINDEX, n);
    }

    //public delegate LuaNumberType LuaLOptDelegate (IntPtr L, int narg);
    //public static LuaNumberType LuaLOpt(IntPtr L, LuaLOptDelegate f, int n, LuaNumberType d) {
    //    return LuaIsNoneOrNil(L, (n != 0) ? d : f(L, n)) ? 1 : 0;}

    //public delegate LuaIntegerType LuaLOptDelegateInteger(IntPtr L, int narg);
    //public static LuaIntegerType LuaLOptInteger(IntPtr L, LuaLOptDelegateInteger f, int n, LuaNumberType d) {
    //    return (LuaIntegerType)(LuaIsNoneOrNil(L, n) ? d : f(L, (n)));
    //}

    /*
    ** {======================================================
    ** Generic Buffer manipulation
    ** =======================================================
    */

    /*
    @@ LUAL_BUFFERSIZE is the buffer size used by the lauxlib buffer system.
    */
    public const int LUAL_BUFFERSIZE = 512;

    public class luaL_Buffer
    {
        public char[] b;  /* buffer address */
        public int size;  /* buffer size */
        public int n;  /* number of characters in buffer */
        public IntPtr L;
        public char[] initb = new char[LUAL_BUFFERSIZE];  /* initial buffer */
    };

    public static void luaL_addchar(luaL_Buffer B, char c)
    {
        if (B.n >= B.size)
            luaL_prepbuffsize(B, 1);
        B.b[B.n++] = c;
    }

    public static void luaL_addsize(luaL_Buffer B, int s)
    {
        B.n += s;
    }

    [DllImport(libname)]
    public static extern void luaL_buffinit(IntPtr L, ref luaL_Buffer B);
    [DllImport(libname)]
    public static extern IntPtr luaL_prepbuffsize(luaL_Buffer B, int sz);
    [DllImport(libname)]
    public static extern void luaL_addlstring(luaL_Buffer B, [MarshalAs(UnmanagedType.LPStr)]string s, int l);
    [DllImport(libname)]
    public static extern void luaL_addstring(luaL_Buffer B, [MarshalAs(UnmanagedType.LPStr)]string s);
    [DllImport(libname)]
    public static extern void luaL_addvalue(luaL_Buffer B);
    [DllImport(libname)]
    public static extern void luaL_pushresult(luaL_Buffer B);
    [DllImport(libname)]
    public static extern void luaL_pushresultsize(luaL_Buffer B, int sz);
    [DllImport(libname)]
    public static extern IntPtr luaL_buffinitsize(IntPtr L, luaL_Buffer B, int sz);
    public static IntPtr luaL_prepbuffer(luaL_Buffer B)
    {
        return luaL_prepbuffsize(B, LUAL_BUFFERSIZE);
    }

    /* }====================================================== */

    ///* compatibility with ref system */

    ///* pre-defined references */
    //public const int LUA_NOREF = (-2);
    //public const int LUA_REFNIL = (-1);

    //public static void lua_ref(IntPtr L, bool lock_)
    //{
    //    if (lock_)
    //    {
    //        luaL_ref(L, LUA_REGISTRYINDEX);
    //    }
    //    else
    //    {
    //        lua_pushstring(L, "unlocked references are obsolete");
    //        lua_error(L);
    //    }
    //}

    //public static void lua_unref(IntPtr L, int nref)
    //{
    //    luaL_unref(L, LUA_REGISTRYINDEX, nref);
    //}

    //public static void lua_getref(IntPtr L, int nref)
    //{
    //    lua_rawgeti(L, LUA_REGISTRYINDEX, nref);
    //}

    /*
    ** {======================================================
    ** File handles for IO library
    ** =======================================================
    */

    /*
    ** A file handle is a userdata with metatable 'LUA_FILEHANDLE' and
    ** initial structure 'luaL_Stream' (it may contain other fields
    ** after that initial structure).
    */
    public const string LUA_FILEHANDLE = "FILE*";

    public class luaL_Stream
    {
        public IntPtr f;  /* stream (NULL for incompletely created streams) */
        public lua_CFunction closef;  /* to close stream (NULL for closed streams) */
    };


    //lualib.h

    public const string LUA_COLIBNAME = "coroutine";
    public const string LUA_TABLIBNAME = "table";
    public const string LUA_IOLIBNAME = "io";
    public const string LUA_OSLIBNAME = "os";
    public const string LUA_STRLIBNAME = "string";
    public const string LUA_BITLIBNAME = "bit32";
    public const string LUA_MATHLIBNAME = "math";
    public const string LUA_DBLIBNAME = "debug";
    public const string LUA_LOADLIBNAME = "package";

    [DllImport(libname)]
    public static extern int luaopen_base(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_coroutine(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_table(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_io(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_os(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_string(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_bit32(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_math(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_debug(IntPtr L);
    [DllImport(libname)]
    public static extern int luaopen_package(IntPtr L);

    /* open all previous libraries */
    [DllImport(libname)]
    public static extern void luaL_openlibs(IntPtr L);

    public static void lua_assert(bool x)
    {
        return;
    }

    //
    public static string geterror(IntPtr L)
    {
        if (lua.lua_isstring(L, -1) != 0)
        {
            string error = lua_tostring(L, -1);
            lua_pop(L, 1);
            return error;
        }
        return "";
    }

    public static string IntPtr2String(IntPtr s)
    {
        return Marshal.PtrToStringAnsi(s);
    }

    //
    public enum ELuaObjType
    {
        EInvalid = 0,
        ENumber = 1,
        EString = 2,
        ETable = 3,
    };

    public class SLuaObjectKey
    {
        private ELuaObjType eType = ELuaObjType.EInvalid;
        private int nKey = 0;
        private string strKey = "";

        public ELuaObjType Type
        {
            get
            {
                return eType;
            }
        }
        public int Num
        {
            get
            {
                return nKey;
            }
            set
            {
                nKey = value;
                eType = ELuaObjType.ENumber;
            }
        }
        public string Str
        {
            get
            {
                return strKey;
            }
            set
            {
                strKey = value;
                eType = ELuaObjType.EString;
            }
        }

        public SLuaObjectKey()
        {
        }
        public SLuaObjectKey(int key)
        {
            eType = ELuaObjType.ENumber;
            nKey = key;
        }
        public SLuaObjectKey(string key)
        {
            eType = ELuaObjType.EString;
            strKey = key;
        }

        public static explicit operator int(SLuaObjectKey key)
        {
            if (key.eType == ELuaObjType.ENumber)
            {
                return key.nKey;
            }
            else
            {
                return 0;
            }
        }

        public static explicit operator string(SLuaObjectKey key)
        {
            if (key.eType == ELuaObjType.EString)
            {
                return key.strKey;
            }
            else
            {
                return null;
            }
        }
    }

    public class SLuaObjectValue
    {
        private ELuaObjType eType = ELuaObjType.EInvalid;
        private float fValue = 0;
        private string strValue = "";
        private Dictionary<SLuaObjectKey, SLuaObjectValue> mapObjectValue = new Dictionary<SLuaObjectKey, SLuaObjectValue>();

        public ELuaObjType Type
        {
            get
            {
                return eType;
            }
        }
        public float Num
        {
            get
            {
                return fValue;
            }
            set
            {
                fValue = value;
                eType = ELuaObjType.ENumber;
            }
        }
        public string Str
        {
            get
            {
                return strValue;
            }
            set
            {
                strValue = value;
                eType = ELuaObjType.EString;
            }
        }
        public Dictionary<SLuaObjectKey, SLuaObjectValue> Tab
        {
            get
            {
                return mapObjectValue;
            }
            set
            {
                mapObjectValue = value;
                eType = ELuaObjType.ETable;
            }
        }

        public SLuaObjectValue()
        {
        }
        public SLuaObjectValue(int val)
        {
            eType = ELuaObjType.ENumber;
            fValue = val;
        }
        public SLuaObjectValue(float val)
        {
            eType = ELuaObjType.ENumber;
            fValue = val;
        }
        public SLuaObjectValue(string val)
        {
            eType = ELuaObjType.EString;
            strValue = val;
        }

        public void Reset()
        {
            fValue = 0;
            strValue = "";
            mapObjectValue.Clear();
        }

        public static explicit operator int(SLuaObjectValue obj)
        {
            if (obj.eType == ELuaObjType.ENumber)
            {
                return (int)obj.fValue;
            }
            else
            {
                return 0;
            }
        }

        public static explicit operator float(SLuaObjectValue obj)
        {
            if (obj.eType == ELuaObjType.ENumber)
            {
                return obj.fValue;
            }
            else
            {
                return 0;
            }
        }

        public static explicit operator string(SLuaObjectValue obj)
        {
            if (obj.eType == ELuaObjType.EString)
            {
                return obj.strValue;
            }
            else
            {
                return null;
            }
        }

        public static explicit operator Dictionary<int, float>(SLuaObjectValue obj)
        {
            if (obj.eType == ELuaObjType.ETable)
            {
                Dictionary<int, float> ret = new Dictionary<int, float>();
                foreach (KeyValuePair<SLuaObjectKey, SLuaObjectValue> kv in obj.mapObjectValue)
                {
                    if (kv.Key.Type == ELuaObjType.ENumber && kv.Value.Type == ELuaObjType.ENumber)
                    {
                        ret.Add((int)kv.Key, (float)kv.Value);
                    }
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

        public static explicit operator Dictionary<string, float>(SLuaObjectValue obj)
        {
            if (obj.eType == ELuaObjType.ETable)
            {
                Dictionary<string, float> ret = new Dictionary<string, float>();
                foreach (KeyValuePair<SLuaObjectKey, SLuaObjectValue> kv in obj.mapObjectValue)
                {
                    if (kv.Key.Type == ELuaObjType.EString && kv.Value.Type == ELuaObjType.ENumber)
                    {
                        ret.Add((string)kv.Key, (float)kv.Value);
                    }
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

        public void GetValue(ref int ret)
        {
            if (eType == ELuaObjType.ENumber)
            {
                ret = (int)fValue;
            }
            else
            {
                ret = 0;
            }
        }

        public void GetValue(ref float ret)
        {
            if (eType == ELuaObjType.ENumber)
            {
                ret = fValue;
            }
            else
            {
                ret = 0;
            }
        }

        public void GetValue(ref string ret)
        {
            if (eType == ELuaObjType.EString)
            {
                ret = strValue;
            }
            else
            {
                ret = null;
            }
        }

        public void GetValue(ref Dictionary<int, float> ret)
        {
            if (eType == ELuaObjType.ETable)
            {
                foreach (var kv in mapObjectValue)
                {
                    if (kv.Key.Type == ELuaObjType.ENumber && kv.Value.Type == ELuaObjType.ENumber)
                    {
                        ret.Add((int)kv.Key, (float)kv.Value);
                    }
                }
            }
            else
            {
                ret.Clear();
            }
        }

        public void GetValue(ref Dictionary<string, float> ret)
        {
            if (eType == ELuaObjType.ENumber)
            {
                foreach (var kv in mapObjectValue)
                {
                    if (kv.Key.Type == ELuaObjType.EString && kv.Value.Type == ELuaObjType.ENumber)
                    {
                        ret.Add((string)kv.Key, (float)kv.Value);
                    }
                }
            }
            else
            {
                ret.Clear();
            }
        }

    }

    public static bool lua_toobjectkey(IntPtr L, int idx, ref SLuaObjectKey key)
    {
        if (lua_isnumber(L, idx) != 0)
        {
            key.Num = (int)lua_tonumber(L, idx);
        }
        else if (lua_isstring(L, idx) != 0)
        {
            //stObjectKey.eType = ELuaStrType;
            key.Str = lua_tostring(L, idx);
        }
        else
        {
            return false;
        }

        return true;
    }

    public static bool lua_toobjectvalue(IntPtr L, int idx, ref SLuaObjectValue obj)
    {
        if (lua_isnumber(L, idx) != 0)
        {
            obj.Num = (float)lua_tonumber(L, idx);
        }
        else if (lua_isstring(L, idx) != 0)
        {
            obj.Str = lua_tostring(L, idx);
        }
        else if (lua_istable(L, idx))
        {
            lua_pushnil(L);//初始键值
            Dictionary<SLuaObjectKey, SLuaObjectValue> t = new Dictionary<SLuaObjectKey, SLuaObjectValue>();
            while (lua_next(L, idx > 0 ? idx : idx - 1) != 0)
            {
                SLuaObjectKey stObjectKey = new SLuaObjectKey();

                if (!lua_toobjectkey(L, -2, ref stObjectKey))
                {
                    continue;
                }

                SLuaObjectValue stObjectValue = new SLuaObjectValue();
                lua_toobjectvalue(L, -1, ref stObjectValue);
                t.Add(stObjectKey, stObjectValue);

                lua_pop(L, 1);
            }
            obj.Tab = t;
        }
        else
        {
            return false;
        }

        return true;
    }

    public static bool lua_pushobjkey(IntPtr L, SLuaObjectKey k)
    {
        switch (k.Type)
        {
            case ELuaObjType.ENumber:
                lua_pushinteger(L, k.Num);
                return true;
            case ELuaObjType.EString:
                lua_pushstring(L, k.Str);
                return true;
            default:
                return false;
        }
    }

    public static bool lua_pushobjvalue(IntPtr L, SLuaObjectValue v)
    {
        switch (v.Type)
        {
            case ELuaObjType.ENumber:
                lua_pushnumber(L, v.Num);
                return true;
            case ELuaObjType.EString:
                lua_pushstring(L, v.Str);
                return true;
            case ELuaObjType.ETable:
                lua_pushtable(L, v.Tab);
                return true;
            default:
                return false;
        }
    }

    public static void lua_pushtable(IntPtr L, int[] arr)
    {
        lua_newtable(L);
        int nTableIndex = lua_gettop(L);

        for (int i=0; i<arr.Length; ++i)
        {
            lua_pushinteger(L, i+1);//key
            lua_pushinteger(L, arr[i]);//val
            lua_settable(L, nTableIndex);
        }
    }

    public static void lua_pushtable(IntPtr L, float[] arr)
    {
        lua_newtable(L);
        int nTableIndex = lua_gettop(L);

        for (int i = 0; i < arr.Length; ++i)
        {
            lua_pushinteger(L, i + 1);//key
            lua_pushnumber(L, arr[i]);//val
            lua_settable(L, nTableIndex);
        }
    }

    public static void lua_pushtable(IntPtr L, string[] arr)
    {
        lua_newtable(L);
        int nTableIndex = lua_gettop(L);

        for (int i = 0; i < arr.Length; ++i)
        {
            lua_pushinteger(L, i + 1);//key
            lua_pushstring(L, arr[i]);//val
            lua_settable(L, nTableIndex);
        }
    }

    public static void lua_pushtable(IntPtr L, List<int> t)
    {
        lua_newtable(L);
        int nTableIndex = lua_gettop(L);

        for (int i = 0; i < t.Count; ++i)
        {
            lua_pushinteger(L, i);//key
            lua_pushnumber(L, t[i]);//val
            lua_settable(L, nTableIndex);
        }
    }

    public static void lua_pushtable(IntPtr L, Dictionary<int, float> t)
    {
        lua_newtable(L);
        int nTableIndex = lua_gettop(L);

        foreach (KeyValuePair<int, float> kv in t)
        {
            lua_pushinteger(L, kv.Key);//key
            lua_pushnumber(L, kv.Value);//val
            lua_settable(L, nTableIndex);
        }
    }

    public static void lua_pushtable(IntPtr L, Dictionary<string, float> t)
    {
        lua_newtable(L);
        int nTableIndex = lua_gettop(L);

        foreach (KeyValuePair<string, float> kv in t)
        {
            lua_pushstring(L, kv.Key);//key
            lua_pushnumber(L, kv.Value);//val
            lua_settable(L, nTableIndex);
        }
    }

    public static void lua_pushtable(IntPtr L, Dictionary<SLuaObjectKey, SLuaObjectValue> t)
    {
        lua_newtable(L);
        int nTableIndex = lua_gettop(L);

        foreach (KeyValuePair<SLuaObjectKey, SLuaObjectValue> kv in t)
        {
            lua_pushobjkey(L, kv.Key);//key
            lua_pushobjvalue(L, kv.Value);//val
            lua_settable(L, nTableIndex);
        }
    }
}

